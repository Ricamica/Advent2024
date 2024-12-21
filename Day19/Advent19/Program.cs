using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Advent19;
string[] fullInput = System.IO.File.ReadAllLines(@"Tinput.txt");

Day19 day19 = new Day19(fullInput);
Console.WriteLine(day19.Part1());
Console.ReadLine();
fullInput = System.IO.File.ReadAllLines(@"input.txt");
day19 = new Day19(fullInput);
Console.WriteLine(day19.Part1());
namespace  Advent19
{   
    public class Day19{
        string[] sortedAvailablePatterns;
        string[] patternsList;
        Dictionary<string,int> a;
        List<string> combSeen;
        public Day19(string[] fullInput){
            sortedAvailablePatterns = fullInput[0].Split(", ");
            patternsList = fullInput[2..];
            Array.Sort(sortedAvailablePatterns, (x1,x2) => x2.Length.CompareTo(x1.Length));
            a= new();
            combSeen = new();

        }
        public int Part1(){
            int possibleDesigns = 0;
            foreach(var oneDesiredPattern in patternsList){
                if(isValidPattern(oneDesiredPattern,sortedAvailablePatterns))
                {
                    //Console.WriteLine(oneDesiredPattern);
                    possibleDesigns++;
                }
            }
            foreach(var dict in a){
                Console.WriteLine(dict);
            }

            return possibleDesigns;
        }
        public bool isValidPattern(string desiredPattern, string[] availableSorted){
            bool DFS(string currPattern, List<string> currPath, string currAdded){
                if(currPattern == desiredPattern){
                
                    if(!a.ContainsKey(currPattern)){
                        
                        a.Add(currPattern,0);
                    }
                    //currPath.Add(currAdded);
                    foreach(var i in currPath){
                        Console.Write(i+"->");
                    }
                    Console.WriteLine(currPattern+ " "+ a[currPattern]);
                    a[currPattern]+=1;
                    //return true; //add back for part1 count
                }
                if(currPattern.Length > desiredPattern.Length){return false;}
                if(!(desiredPattern[..currPattern.Length] == currPattern)){return false;}
                foreach(var onePattern in availableSorted){
                    //currPath.Add(onePattern);
                    //if(onePattern == currAdded){continue;}
                    bool isValid = DFS(currPattern+onePattern,  currPath,onePattern);
                    //currPath.Remove(onePattern);
                    if(isValid){
                        return true;
                    }
                }
                return false;
            }
            
            bool isValid = false;
            foreach(var onePattern in availableSorted){
                if(desiredPattern.Contains(onePattern)){
                    List<string> onePath = new();
                    string addedPattern = onePattern;
                    isValid = DFS(onePattern,onePath,addedPattern);
                    if(isValid){
                        return true;
                    }
                }
                else{
                    continue;
                }
            }

            
            return false;
        }


    }
    
}