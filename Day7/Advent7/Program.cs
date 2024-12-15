using System.Text.RegularExpressions;

Program p = new Program();
p.Part1();
public partial class Program{
    string[]
    
    input = System.IO.File.ReadAllLines(@"input.txt");
    
    public Program(){
        
    }

    public void Part1(){
        long totalReult = 0;
        foreach(var line in input){
            var parts = line.Split(": ", StringSplitOptions.TrimEntries);
            long target = long.Parse(parts[0]);
            long[] operators = parts[1].Split(" ").Select(long.Parse).ToArray();
            long result = 0;

            bool Matches(long[] zbytekCisel, long total){
                if(zbytekCisel.Length == 0){
                    if(total == target){
                        return true;
                    }
                    else{
                        return false;
                    }
                }
                var remainingArray = zbytekCisel.Skip(1).ToArray();
                return Matches(remainingArray, total*zbytekCisel[0]) || Matches(remainingArray, total+zbytekCisel[0]);
            }
            bool isMatch = Matches(operators,result);
            
            if(isMatch){
                totalReult+=target;
            }
        }
        Console.WriteLine(totalReult);
    }
}
