using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot
{
    public static class ChatBotDataManager
    {
        // Load data into the decision tree
        public static Node LoadTree()
        {
            Node root = new Node
            {
                ButtonLabel = "empty",
                LabelText = "empty",
                Response = "Hi! What can I help you with?"
            };

            Node node1 = new Node
            {
                ButtonLabel = "Payment Issues",
                LabelText = "I have a problem with buying a new item.",
                Response = "Can you specify what issue you came accross during the buying process?"
            };

            Node node2 = new Node
            {
                ButtonLabel = "Selling Issues",
                LabelText = "I have a problem with selling an item.",
                Response = "Can you specify what issue you came accross during the selling process?"
            };

            Node node3 = new Node
            {
                ButtonLabel = "Account & Security",
                LabelText = "I have a problem with my account / I ran into a scammer.",
                Response = "Can you specify what issue you have regarding your account or security?"
            };

            AddChildren([root], [node1, node2, node3]);

            Node node11 = new Node
            {
                ButtonLabel = "Item not as described",
                LabelText = "I bought an item but it's different from what I listed. Can I get a refund?",
                Response = "If you bought an item that is different from the one listed you should go to the listing an immediately report it." +
                "If you want to negotiate a refund, " +
                "please reach out to our live customer support. If your case is not available for a refund, try negotiating with the seller himself, if possible."
            };

            Node node12 = new Node
            {
                ButtonLabel = "Seller not responding",
                LabelText = "The seller is not responding to any of my messages. What should I do?",
                Response = "Wait for the seller to respond, people can take a while to check their messages. If you already waited for a long time, you could report the " +
                "seller for being inactive and we will try to contact them. Otherwise we will remove their listing as soon as possible."
            };

            Node node13 = new Node
            {
                ButtonLabel = "Payment Issues",
                LabelText = "What are the payment methods? I payed for an item but I never received it.",
                Response = "The payment method depends on the seller. If it's not clarified in the listing what payment methods are accepted by the seller, you should " +
                "contact them."
            };

            AddChildren([node1], [node11, node12, node13]);

            Node node21 = new Node
            {
                ButtonLabel = "Buyer not showing up",
                LabelText = "What should I do if a buyer agreed to meet but never showed up?",
                Response = "In case you agreed to meet with a buyer but they never showed up, you should try contacting them to find out what happened. " +
                "You could arrange for a new meeting if both of you are open to it. In case you can't reach them, just let them go. We understand it's frustrating to have someone " +
                "waste your time but unfortunately it happens."
            };

            Node node22 = new Node
            {
                ButtonLabel = "Payment disputes",
                LabelText = "The buyer claims they sent payment, but I never received the money. What should I do?",
                Response = "If the buyer says they sent payment, but you have not received the money, here are the steps you should follow.\n" +
                "1. Wait some time, it's possible that the bank transfer is still pending.\n" +
                "2. Make sure the buyer sent the money. Ask them for proof if possible.\n" +
                "3. Contact your bank to see if there are any pending transfers.\n" +
                "If none of these steps solved your problem, you are probably dealing with a scammer. Feel free to report them and we will review the case!"
            };

            Node node23 = new Node
            {
                ButtonLabel = "Listing getting removed or rejected",
                LabelText = "Why was my listing removed? What are the rules for selling an item?",
                Response = "If your listing was removed from the store, it's possible that it did not comply with our policy, or it was a fraudulent listing. To find out why " +
                "your listing was removed, please contact our live customer support. You can also check out our rules for selling an item by reading our sellers policy statement."
            };

            AddChildren([node2], [node21, node22, node23]);

            Node node31 = new Node
            {
                ButtonLabel = "Account access issues",
                LabelText = "I can't log into my account or it was restricted. How can I fix this?",
                Response = "If you have issues logging in or your account was restricted here are the steps you could follow to potentially solve the issue:\n" +
                "1. Make sure your login credintials are correct when trying to log in.\n" +
                "2. Press the account recovery button and try to recover your account that way.\n" +
                "3. If you still can't log in, or your account was restricted, please contact our live customer support for further help."
            };

            Node node32 = new Node
            {
                ButtonLabel = "Suspicious messages & scams",
                LabelText = "I received a suspicious message from a buyer/seller. How can I stay safe?",
                Response = "Scammers are present everywhere, even on our store. If you think you bumped into a scammer or someone suspicious, you should report them to us " +
                "and we will review their account. How can you stay safe from scammers? It is essential to not give any sensitive information away. Do not send people money extra money " +
                "other than what the price of the item is. If someone has scammed you, you should immediately report them and talk to our live customer support to see if " +
                "you are available for a refund."
            };

            Node node33 = new Node
            {
                ButtonLabel = "Scam or fraudelent seller",
                LabelText = "I payed for an item but never received it. What can I do?",
                Response = "If you payed for an item but never received it you should follow the following steps:\n" +
                "1. Try to find out more about your items whereabouts from the seller.\n" +
                "2. If they are ignoring you for a long time or blocked you, immediately report them for being fraudulent!\n" +
                "3. Go and talk to our live customer support to try and get a refund."
            };

            Node node34 = new Node
            {
                ButtonLabel = "Fake or misleading listings",
                LabelText = "How can I report a listing that is scam or fake?",
                Response = "If you are sure that a listing is a scam or is fake, you could report it by clicking on the report button in the listing."
            };

            AddChildren([node3], [node31, node32, node33, node34]);

            Node restartNode = new Node
            {
                ButtonLabel = "Restart conversation",
                LabelText = "There are no further available options for you to choose from. Would you like to restart this conversation?",
                Response = "Hi! What can I help you with?"
            };

            AddChildren([node11, node12, node13, node21, node22, node23, node31, node32, node33, node34], [restartNode]);
            restartNode.Children = root.Children; // Connect end node with the root

            return root;
        }

        private static void AddChildren(List<Node> nodes, List<Node> children)
        {
            foreach (Node node in nodes)
            {
                foreach (Node child in children)
                {
                    node.Children.Add(child);
                }
            }
        }
    }
}
