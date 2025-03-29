-- EXECUTE THE ENTIRE FILE TO HAVE THE TABLES WITH THEIR DATA IN THE DATABASE --
-- NO NEED FOR SEPARATE EXECUTIONS --

/* DO NOT EXECUTE THIS UNLESS YOU MESSED UP */
 --DROP TABLE ChatBotChildren
 --DROP TABLE ChatBotNodes
 /* --------------------------------------- */

 /* EXECUTE TO HAVE THESE IN THE DATABASE */
CREATE TABLE ChatBotNodes
(
	pid INT PRIMARY KEY,
	button_label NVARCHAR(100) NOT NULL,
	label_text NVARCHAR(MAX) NOT NULL,
	response_text NVARCHAR(MAX) NOT NULL,
)

CREATE TABLE ChatBotChildren
(
	cid INT PRIMARY KEY IDENTITY,
	parentID INT FOREIGN KEY REFERENCES ChatBotNodes(pid) NOT NULL,
	childID INT FOREIGN KEY REFERENCES ChatBotNodes(pid) NOT NULL
)

-- Insert bot data into nodes
INSERT INTO ChatBotNodes (pid, button_label, label_text, response_text)
VALUES 
(1, 'empty', 'empty', 'Hi! What can I help you with?'), -- Root node cid = 1
(2, 'Payment Issues', 'I have a problem with buying a new item.', 'Can you specify what issue you came accross during the buying process?'), -- Child 1 of Root cid = 2
(3, 'Selling Issues', 'I have a problem with selling an item.', 'Can you specify what issue you came accross during the selling process?'), -- Child 2 of Root cid = 3
(4, 'Account & Security', 'I have a problem with my account / I ran into a scammer.', 'Can you specify what issue you have regarding your account or security?'), -- Child 3 of Root cid = 4

(5, 'Item not as described', 'I bought an item but it''s different from what I listed. Can I get a refund?', 
 'If you bought an item that is different from the one listed you should go to the listing an immediately report it. If you want to negotiate a refund, please reach out to our live customer support. ' + CHAR(13) + CHAR(10) +
 'If your case is not available for a refund, try negotiating with the seller himself, if possible.'), -- Child 1 of Child 1 of Root cid = 5

(6, 'Seller not responding', 'The seller is not responding to any of my messages. What should I do?',  
 'Wait for the seller to respond, people can take a while to check their messages. If you already waited for a long time, you could report the seller for being inactive and we will try to contact them. ' + CHAR(13) + CHAR(10) +
 'Otherwise we will remove their listing as soon as possible.'), -- Child 2 of Child 1 of Root cid = 6

(7, 'Payment Issues', 'What are the payment methods? I payed for an item but I never received it.', 
 'The payment method depends on the seller. If it''s not clarified in the listing what payment methods are accepted by the seller, you should contact them.'), -- Child 3 of Child 1 of Root cid = 7

(8, 'Buyer not showing up', 'What should I do if a buyer agreed to meet but never showed up?', 
 'In case you agreed to meet with a buyer but they never showed up, you should try contacting them to find out what happened. You could arrange for a new meeting if both of you are open to it. ' + CHAR(13) + CHAR(10) +
 'In case you can''t reach them, just let them go. We understand it''s frustrating to have someone waste your time but unfortunately it happens.'), -- Child 1 of Child 2 of Root cid = 8

 (9, 'Payment disputes', 'The buyer claims they sent payment, but I never received the money. What should I do?', 
 'If the buyer says they sent payment, but you have not received the money, here are the steps you should follow.' + 
 '1. Wait some time, it''s possible that the bank transfer is still pending.' + CHAR(13) + CHAR(10) +
 '2. Make sure the buyer sent the money. Ask them for proof if possible.' + CHAR(13) + CHAR(10) +
 '3. Contact your bank to see if there are any pending transfers.' + CHAR(13) + CHAR(10) +
 'If none of these steps solved your problem, you are probably dealing with a scammer. Feel free to report them and we will review the case!'), -- Child 2 of Child 2 of Root cid = 9

(10, 'Listing getting removed or rejected', 'Why was my listing removed? What are the rules for selling an item?', 
 'If your listing was removed from the store, it''s possible that it did not comply with our policy, or it was a fraudulent listing.' + CHAR(13) + CHAR(10) +
 'To find out why your listing was removed, please contact our live customer support. You can also check out our rules for selling an item by reading our sellers policy statement.'), -- Child 3 of Child 2 of Root cid = 10

(11, 'Account access issues', 'I can''t log into my account or it was restricted. How can I fix this?', 
 'If you have issues logging in or your account was restricted here are the steps you could follow to potentially solve the issue:' + CHAR(13) + CHAR(10) +
 '1. Make sure your login credentials are correct when trying to log in.' + CHAR(13) + CHAR(10) +
 '2. Press the account recovery button and try to recover your account that way.' + CHAR(13) + CHAR(10) +
 '3. If you still can''t log in, or your account was restricted, please contact our live customer support for further help.'), -- Child 1 of Child 3 of Root cid = 11

(12, 'Suspicious messages & scams', 'I received a suspicious message from a buyer/seller. How can I stay safe?', 
 'Scammers are present everywhere, even on our store. If you think you bumped into a scammer or someone suspicious, you should report them to us and we will review their account. ' + CHAR(13) + CHAR(10) +
 'How can you stay safe from scammers? It is essential to not give any sensitive information away. Do not send people extra money other than what the price of the item is. ' + CHAR(13) + CHAR(10) +
 'If someone has scammed you, you should immediately report them and talk to our live customer support to see if you are available for a refund.'), -- Child 2 of Child 3 of Root cid = 12

(13, 'Scam or fraudulent seller', 'I paid for an item but never received it. What can I do?', 
 'If you paid for an item but never received it you should follow the following steps:' + CHAR(13) + CHAR(10) +
 '1. Try to find out more about your item''s whereabouts from the seller.' + CHAR(13) + CHAR(10) +
 '2. If they are ignoring you for a long time or blocked you, immediately report them for being fraudulent!' + CHAR(13) + CHAR(10) +
 '3. Go and talk to our live customer support to try and get a refund.'), -- Child 3 of Child 3 of Root cid = 13

(14, 'Fake or misleading listings', 'How can I report a listing that is scam or fake?', 
 'If you are sure that a listing is a scam or is fake, you could report it by clicking on the report button in the listing.'), -- Child 4 of Child 3 of Root cid = 14

 (15, 'Restart conversation', 'There are no further available options for you to choose from. Would you like to restart this conversation?', 
 'Hi! What can I help you with?'); -- Child of all the last nodes cid = 15

 -- Define parent-child relationships
INSERT INTO ChatBotChildren (parentID, childID) VALUES
(1, 2), (1, 3), (1, 4), -- Assign children to root
(2, 5), (2, 6), (2, 7), -- Assign children to child 1 of root
(3, 8), (3, 9), (3, 10), -- Assign children to child 2 of root
(4, 11), (4, 12), (4, 13), (4, 14), -- Assign children to child 3 of root
(5, 15), (6, 15), (7, 15), (8, 15), (9, 15), (10, 15), (11, 15), (12, 15), (13, 15), (14, 15), -- Assign the restart option to all of the end nodes
(15, 2), (15, 3), (15, 4) -- Assign the restart node the roots children