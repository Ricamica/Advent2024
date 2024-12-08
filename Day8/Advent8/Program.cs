using System.Security.Cryptography.X509Certificates;
using Advent8;
string[] input = System.IO.File.ReadAllLines(@"Tinput.txt");

Day8 program = new Day8(input);

Console.WriteLine(program.Part1());

namespace Advent8{    
    public class Day8(string[] input){
        
        public int lengthX = input[0].Length;
        public int lengthY = input.Length;
        public List<(int,int)> listAntinodes = [];
        public int count = 0;

        public int Part1(){
        int list = 0;
        for(int column = 0; column<input.Length;column++){
            for(int row = 0; row<input[0].Length;row++){
                if(input[column][row].ToString()!="."){
                    list = GetAntinodeList(input[column][row].ToString(),row,column,input);
                }        
            }
        }
        return list;
        }

        public (int,int)[] GetAntinodeLocations((int,int) firstAntena,(int,int) secondAntena){
            int firstAntenaX = firstAntena.Item1; 
            int firstAntenaY = firstAntena.Item2; 
            
            int secondAntenaX = secondAntena.Item1; 
            int secondAntenaY = secondAntena.Item2; 
            
            int newX = firstAntenaX - secondAntenaX;
            int newY = firstAntenaY - secondAntenaY;
            (int,int) firstAntinode = (firstAntenaX+newX,firstAntenaY+newY);
            
            newX *= -1;
            newY *= -1;
            (int,int) secondAntinode = (secondAntenaX+newX, secondAntenaY+newY);
            return [firstAntinode,secondAntinode];
        }
        public bool OutOfBounds(int X, int Y){
            bool outOfBounds = X >= this.lengthX || X < 0 ||
            Y >= this.lengthY || Y <0;
            return outOfBounds;
        }
    

        public int GetAntinodeList(string currentAntena, int startX, int startY, string[] input){
            
            for(int Y = startY; Y<this.lengthY;Y++){
                for(int X = 0; X<this.lengthX;X++){
                    if(input[Y][X].ToString() != "." && input[Y][X].ToString() == currentAntena && !(X==startX && Y==startY)){
                        var a = GetAntinodeLocations((startX,startY),(X,Y));
                        bool tupleHadProduct = listAntinodes.Any(m => m == a[0] || listAntinodes.Any(m => m == a[1] ));
                        
                        if( !listAntinodes.Any(m => m == a[0])){
                            if(!OutOfBounds(a[0].Item1,a[0].Item2)){
                                count++;
                                listAntinodes.Add(a[0]);
                            }
                            
                        }
                        if( !listAntinodes.Any(m => m == a[1]) ){
                            if(!OutOfBounds(a[1].Item1,a[1].Item2)){
                                count++;
                                listAntinodes.Add(a[1]);
                            }
                        }
                    }
                }
            }
            return count;   
        }
    }    
}