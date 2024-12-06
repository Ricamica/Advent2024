// See https://aka.ms/new-console-template for more information
using System.Data;
using System.IO.Pipelines;
using System.Text;

string[] lines = System.IO.File.ReadAllLines(@"input.txt");
//lines = System.IO.File.ReadAllLines(@"testinput.txt");

string dirrection = "up";
int x,y,startX,startY;
x=y=0;
for(int i = 0; i<lines.Length; i++){
    if(lines[i].Contains("^")){
      y=i;
      x=lines[i].IndexOf("^");
      break;  
    }
}

startX=x;
startY=y;

List<(int,int)> listNavstivenych = [(x,y)];
List<(int,int,string)> listNavstivenychBloku = [(x,y,dirrection)];
int delkaY = lines.Length;
int delkaX = lines[0].Length;
int result =1;
bool isLoop = false;

Dictionary<int,(int,int)> Directions = new Dictionary<int,(int,int)>();
Directions.Add(0,(1,0));
Directions.Add(1,(0,1));
Directions.Add(2,(-1,0));
Directions.Add(3,(0,-1));

Console.WriteLine(4%4);
int numLoop =0;
void checkInput(){
    int step = 0;
    while(y>=0 && y<= lines.Length && x>=0 && x<= lines[0].Length && !isLoop){
        switch(dirrection){
            case "up":{
                if(lines[y-1][x].ToString() == "#"){
                    if(listNavstivenychBloku.Contains((y-1,x,dirrection))){
                        isLoop = true;
                    }
                    else{
                        listNavstivenychBloku.Add((y-1,x,dirrection));
                        dirrection = "right";
                        break;
                    }

                    
                }
                if(lines[y][x].ToString() != "X"){

                    result++;
                }
                zapisKrok();
                y--;
                
                if(isLoop){
                    numLoop++;
                }
                break;
            }
            case "down":{
                if(lines[y+1][x].ToString() == "#"){
                    if(listNavstivenychBloku.Contains((y+1,x,dirrection))){
                        isLoop = true;
                    }
                    else{
                        listNavstivenychBloku.Add((y+1,x,dirrection));
                        dirrection = "left";
                        break;
                    }
                }
                if(lines[y][x].ToString() != "X"){
                    result++;
                }
                zapisKrok();
                y++;
                
                if(isLoop){
                    numLoop++;
                }
                break;
            }
            case "right":{
                if(lines[y][x+1].ToString() == "#"){
                    if(listNavstivenychBloku.Contains((y,x+1,dirrection))){
                        isLoop = true;
                    }
                    else{
                        listNavstivenychBloku.Add((y,x+1,dirrection));
                        dirrection = "down";
                        break;
                    }
                }
                if(lines[y][x].ToString() != "X"){
                    result++;
                }
                zapisKrok();
                x++;
                
                if(isLoop){
                    numLoop++;
                }
                break;
            }
            
            case "left":{
                if(lines[y][x-1].ToString() == "#"){
                    if(listNavstivenychBloku.Contains((y,x-1,dirrection))){
                        isLoop = true;
                    }
                    else{
                        listNavstivenychBloku.Add((y,x-1,dirrection));
                        dirrection = "up";
                        break;
                    }
                }
                if(lines[y][x].ToString() != "X"){
                    result++;
                }
                zapisKrok();
                x--;
                
                if(isLoop){
                    numLoop++;
                }
                break;
            }
        }
        if(y+1 >= delkaY || y-1 == -1 || x+1 >= delkaX || x-1 == -1){
            zapisKrok(); 
            //Console.WriteLine(result);
            isLoop = false;
            break;
        }
        
    }
}

void vypis(){
    foreach(string line in lines){
        Console.WriteLine(line);

    }
    Console.WriteLine();
}
void zapisKrok(){
    StringBuilder sb = new StringBuilder(lines[y]);
    sb[x] = "X".ToCharArray()[0];
    string modString = sb.ToString();
    listNavstivenych.Add((x,y));
    lines[y]=modString;
}

void zapisNovzBloker(int x, int y){
    if(x!=startX && y!=startY){
        StringBuilder sb = new StringBuilder(lines[y]);
        sb[x] = "#".ToCharArray()[0];
        string modString = sb.ToString();
        lines[y]=modString;
    }
}
checkInput();
Console.WriteLine(result);
numLoop=0;
var navstiveni = listNavstivenych;
foreach(var a in navstiveni){
    lines = System.IO.File.ReadAllLines(@"input.txt");
    dirrection = "up";

    x=y=0;
    for(int i = 0; i<lines.Length; i++){
        if(lines[i].Contains("^")){
        y=i;
        x=lines[i].IndexOf("^");
        break;  
        }
    }

    listNavstivenych = [(x,y)];
    listNavstivenychBloku = [(x,y,dirrection)];
    delkaY = lines.Length;
    delkaX = lines[0].Length;
    result =1;
    isLoop = false;
    Directions = new Dictionary<int,(int,int)>();
    Directions.Add(0,(1,0));
    Directions.Add(1,(0,1));
    Directions.Add(2,(-1,0));
    Directions.Add(3,(0,-1));

    int X = a.Item1;
    int Y = a.Item2;
    zapisNovzBloker(X,Y);
    checkInput();
}
Console.WriteLine(result);
Console.WriteLine(numLoop);
