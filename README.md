# Imandra Technical Interview

We'd appreciate it if you didn't share this publicly - thanks in advance.

## Introduction

FIX ([Financial Information eXchange](https://en.wikipedia.org/wiki/Financial_Information_eXchange)) is a communications protocol commonly used in finance, to allow different parties to communicate with each other. Don't worry if you've never heard of it - just know that it's a simple protocol, designed to allow financial institutions to integrate with each other. Here's an example message:

```
8=FIX.4.4|9=107|35=D|49=ClientA|56=TradingPlatform|52=20230608-07:33:22.102|38=38|55=AAPL|11=14070885|54=1|44=17|40=2|59=0|10=296|
```

- Messages in the FIX protocol are made up of fields.
- Each field is a tag/value pair, separated by the `=` character. For example, `35=D` states that tag `35` has value `D`:
  - by specification, tag `35` declares the `MsgType` field (the type of message currently being sent), and
  - the value `D` refers to a `MsgType` of `NewOrderSingle` (a common message used to submit an order to a trading platform to buy or sell shares), so
  - `35=D` states that the current message is a `NewOrderSingle` message.
- Each field is separated from the next by a separator character:
  - usually this is the character `0x01`, but
  - throughout this exercise the separator character is represented by `|` to simplify things slightly.
- Each FIX message starts with a header:
  - the tag `8 (BeginString)`, with a fixed value stating the current version of FIX being used. In this exercise, we will always see `8=FIX4.4`
  - the tag `9 (BodyLength)`, with an integer value stating the length of the `body` of the message.
- Next comes the `body` of the message: a sequence of fields, starting with its type in the `35 (MsgType)` field. In the case of the `NewOrderSingle` message, the body fields contain information about the order being submitted, for example how large the order is, whether the trader is trying to buy or sell, and what price they are willing to trade at.
- Finally, there's a message trailer field: `10 (Checksum)`. This field tag and its value do not contribute to the `9 (BodyLength)` in the header.
  - Note that the value of the checksum isn't relevant for the purposes of this exercise, and is randomized in the test data.

In the `data` folder of this project, there are two files:
- `fix.log`: this is a log of a FIX session, which we'll use as the input for your solution.
- `tags.json`: this is a dictionary of tag numbers to their human readable names, along with any relevant enum values for each tag.

The FIX session itself has the following properties:
- the session contains messages between various clients and a trading platform server, where they are trying to trade with each other by submitting orders to the platform
- a client is able to send various messages:
  - messages with `35 (MsgType) = D (NewOrderSingle)` create new orders.
  - messages with `35 (MsgType) = F (OrderCancelRequest)` cancel existing orders.
  - messages with `35 (MsgType) = G (OrderCancelReplaceRequest)` update existing orders.
  - each message that the client sends has a unique ID: the `11 (ClOrdID)` (short for 'client order ID').
  - `MsgType`s `F` and `G` both refer to a previous order using the `41 (OrigClOrdID)` field.
- the server sends `35 (MsgType) = 8 (ExecutionReport)` messages, stating what has happened to a particular order (by referencing its `ClOrdID`) via the `150 (ExecType)` field:
  - `0 (New)` indicates a new order was created successfully by a `NewOrderSingle`.
  - `4 (Canceled)` indicates an existing order was canceled by an `OrderCancelRequest`.
  - `5 (Replaced)` indicates an existing order was updated by an `OrderCancelReplaceRequest`.
  - `8 (Rejected)` indicates a request to create/update/cancel an order was rejected.
  - `F (Filled)` indicates an order traded with another order. The `39 (OrdStatus)` tag shows whether the order was only partially filled or fully-filled (`1 (Partially filled)` vs `2 (Fully filled)`).
- some of the client's messages trigger a single message in reply, whereas sometimes multiple messages are sent in reply.
- each client submits both buy `54 (Side) = 1 (BUY)` and sell `54 (Side) = 2 (SELL)` orders and receives notifications of trades between these two sides.
- clients can trade with themselves.
- when a trade happens, the tag `880 (TrdMatchID)` contains a unique ID for that trade, which is sent to both sides.

## Your solution

### Setting up

- Feel free to use your language of choice.
- Please create a new git repository for your solution.
- Commit at regular intervals showing how your solution evolved.
- Please only spend a few hours on this as your time allows! The idea is for your work to form the basis of a discussion where you walk us through your solution so far, and describe any trade-offs you made while working on the solution (including those made due to time constraints). As part of that discussion, be ready to describe your solution, answer questions about your approach, and talk about any improvements you would have made given more time.
- Use any libraries you like while implementing your solution. Be ready to discuss why you decided to use those libraries.

### Part 1

Write a small CLI tool to load the file `data/fix.log` based on the description above, and print the individual messages to the console. Use the `tags.json` to make the output a bit friendlier to view.

You should be able to run something similar to the below:

`./my-solution.exe print-messages -f data/fix.log --tags data/tags.json`

and get output similar to:

```
{ "BeginString": "FIX.4.4", "BodyLength": 123, "MsgType": "NewOrderSingle", ... }
{ "BeginString": "FIX.4.4", "BodyLength": 123, "MsgType": "ExecReport", ... }
...
```

### Part 2

Answer the following questions, using your code from the previous part as a basis:

1. How many new orders (messages with `35 (MsgType) = D (NewOrderSingle)` were submitted to the exchange?
2. How many were accepted? You can identify accepted orders via `150 (ExecType) = 0 (NEW)` in messages sent by the exchange.
3. What was the total amount traded in the whole session? You can identify trades via `150 (ExecType) = F (TRADE)`, and the amount traded via the value in `32 (LastQty)`.

Please implement each answer as a CLI command.

You should be able to run something similar to the below:

`./my-solution.exe part2-q2 -f fix.log`

and get output displaying the answer to the question.

### Part 3

With the knowledge that:
- Orders are added to the trading platform's book of orders after acknowledgement of a `NewOrderSingle` message.
- Orders are removed from the book after either:
  - Acknowledgement of a cancellation by a `OrderCancelRequest` message
  - Being fully filled.

Implement an additional CLI command to answer the following question:

1. How many times was the book empty on both sides at the same time?

You should be able to run something similar to the below:

`./my-solution.exe part3-q1 -f fix.log`

and get output displaying the answer to the question.

### Submitting your solution

To submit your solution, you can either
- push to a private Github repository, and then add collaborators (@actionshrimp, @mattjbray and @bronsa), or
- send us the whole of your working copy as an email attachment, as a `.zip` file (or similar). Make sure to include the `.git` folder.
