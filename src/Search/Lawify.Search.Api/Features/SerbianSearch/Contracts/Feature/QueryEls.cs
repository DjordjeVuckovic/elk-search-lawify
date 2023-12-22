// public class ElasticsearchQueryBuilder
// {
//     public QueryContainer BuildQuery(string userInput)
//     {
//         var tokens = Tokenize(userInput);
//         var postfixTokens = ConvertToPostfix(tokens);
//         return ConstructQuery(postfixTokens);
//     }
//
//     private List<string> Tokenize(string input)
//     {
//         // Tokenize the input string
//         // e.g., split by space, identify phrases, operators, etc.
//     }
//
//     private List<string> ConvertToPostfix(List<string> tokens)
//     {
//         // Convert the tokens from infix to postfix notation
//         // Use a stack-based algorithm
//     }
//
//     private QueryContainer ConstructQuery(List<string> postfixTokens)
//     {
//         // Construct the Elasticsearch query using the postfix tokens
//         var queryStack = new Stack<QueryContainer>();
//         foreach (var token in postfixTokens)
//         {
//             // Handle different types of tokens and build the query
//         }
//
//         return queryStack.Pop();
//     }
// }