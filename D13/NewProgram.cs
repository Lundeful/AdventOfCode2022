// using System.Text.Json;
// using System.Text.Json.Nodes;
//
// var input = File.ReadLines("input.txt").ToList();
// var testInput = File.ReadLines("testInput.txt").ToList();
//
// var a = GetIndices(testInput); // Should be 13
// var b = GetIndices(input); // Should be ???
//
// // 463, 464, 890 <-- Too low
// // 1246, 890, 1179, 1072, 654, 1047, 940 <-- wrong
//
// Console.WriteLine("Sum of indices test: " + a.Sum());
// Console.WriteLine("Sum of indices real: " + b.Sum());
//
// IEnumerable<int> GetIndices(IReadOnlyList<string> text)
// {
//     var pairs = ParseInput(text);
//     var indices = new List<int>();
//     for (var i = 1; i <= pairs.Count; i++)
//     {
//         var pair = pairs[i-1];
//         var leftArray = JsonSerializer.Deserialize<JsonArray>(pair.left);
//         var rightArray = JsonSerializer.Deserialize<JsonArray>(pair.right);
//         var isCorrect = CompareJsonArrays(leftArray, rightArray, true);
//         if (isCorrect) indices.Add(i);
//     }
//     return indices;
// }
//
// bool CompareJsonArrays(JsonArray left, JsonArray right, bool isTopLevel = false)
// {
//     if (IsNumbersArray(left) && IsNumbersArray(right))
//     {
//         if (isTopLevel && left.Count > right.Count) return false;
//         var isCorrect = CompareListsOfNumbers(
//             left.Select(e => e.GetValue<JsonElement>().GetInt32()).ToList(),
//             right.Select(e => e.GetValue<JsonElement>().GetInt32()).ToList());
//         return isCorrect;
//     }
//     
//     for (var i = 0; i < left.Count; i++)
//     {
//         var leftNode = left[i];
//         if (i >= right.Count) return false;
//         var rightNode = right[i];
//         if (leftNode == null || rightNode == null) throw new Exception("NUULL");
//         if (leftNode is JsonArray && rightNode is JsonArray)
//         {
//             if (leftNode.AsArray().Count > rightNode.AsArray().Count) return false;
//             var isCorrect = CompareJsonArrays(leftNode.AsArray(), rightNode.AsArray());
//             if (!isCorrect)
//             {
//                 return false;
//             }
//         } 
//         else if (leftNode is JsonArray && rightNode is JsonValue)
//         {
//             var convertedRight =  new JsonArray { rightNode.GetValue<JsonElement>().Clone()};
//             var isCorrect = CompareJsonArrays(leftNode.AsArray(), convertedRight);
//             if (!isCorrect)
//             {
//                 return false;
//             }
//         }
//         else if (leftNode is JsonValue && rightNode is JsonArray)
//         {
//             var convertedLeft =  new JsonArray { leftNode.GetValue<JsonElement>().Clone()};
//             var isCorrect = CompareJsonArrays(convertedLeft, rightNode.AsArray());
//             if (!isCorrect)
//             {
//                 return false;
//             }
//         }
//         else
//         {
//             var lv = leftNode.GetValue<JsonElement>().GetInt32();
//             var rv = rightNode.GetValue<JsonElement>().GetInt32();
//             if (lv > rv) return false;
//         }
//     }
//     return true;
// }
//
// bool IsNumbersArray(JsonNode arr) => arr is JsonArray && arr.AsArray().All(e => 
//     e is JsonValue && e.GetValue<JsonElement>().ValueKind == JsonValueKind.Number);
//
// bool CompareListsOfNumbers(IReadOnlyList<int> l, IReadOnlyList<int> r)
// {
//     for (var i = 0; i < Math.Min(l.Count, r.Count); i++)
//     {
//         var lv = l[i];
//         var rv = r[i];
//         if (lv > rv) return false;
//     }
//     
//     return true;
// }
//
// List<(string left, string right)> ParseInput(IReadOnlyList<string> lines)
// {
//     var newPairs = new List<(string left, string right)>();
//     for (var i = 0; i < lines.Count; i++)
//     {
//         var top = lines[i++];
//         var bottom = lines[i++];
//         newPairs.Add((top, bottom));
//     }
//     return newPairs;
// }