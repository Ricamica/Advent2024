using System.IO.Pipelines;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

string[] fullInput = System.IO.File.ReadAllLines(@"input.txt");
Day14 day14 = new Day14(fullInput);
day14.Part1();
public class Day14{

    int sizeX, sizeY;
    string[] fullInput;
    int q1,q2,q3,q4;
    public Day14(string[] aFullInput){
        sizeX = 101;
        sizeY = 103;
        q1=q2=q3=q4= 0;
        fullInput = aFullInput;
    }

    public void Part1(){
        foreach(string oneInput in fullInput){
            var formInput = FormatInput(oneInput);
            int x = formInput.Item1;
            int y =formInput.Item2;
            int xV = formInput.Item3;
            int yV = formInput.Item4;
        //    Console.WriteLine(formInput);

            Hundered_Sec(x,y,xV,yV);
        }
        //Console.WriteLine(q1*q2*q3*q4);
    }

    public (int,int,int,int) FormatInput(string oneInput){
        string pattern = @"p=(\d+),(\d+) v=(-?\d+),(-?\d+)";
        Regex regex = new Regex(pattern);
        MatchCollection matches = regex.Matches(oneInput);
        int posX  = int.Parse(matches[0].Groups[1].Value);
        int posY  = int.Parse(matches[0].Groups[2].Value);
        int X  = int.Parse(matches[0].Groups[3].Value);
        int Y  = int.Parse(matches[0].Groups[4].Value);
        return (posX,posY,X,Y);
    }

    public (int,int) Hundered_Sec_In_One(int posX, int posY, int vX, int vY){
        int newPosX = posX + ((vX*1) % sizeX);
        int newPosY = posY + ((vY*1) % sizeY);
        //Console.WriteLine((newPosX,newPosY));
        return (newPosX, newPosY);
        
    }
    public (int,int) Hundered_Sec(int posX, int posY, int vX, int vY){
        //Console.Write((posX,posY));

        for(int i =0;i<100;i++){
            var result = StepPerSecond(posX,posY,vX,vY);
            posX=result.Item1;
            posY=result.Item2;
        }
        //Console.WriteLine(" "+(posX,posY));
        if(posX <(int)sizeX/2 && posY <(int)sizeY/2){
            q1++;
        }
        else if(posX >(int)sizeX/2 && posY <(int)sizeY/2)
        {
            q2++;
        }
        else if(posX <(int)sizeX/2 && posY >(int)sizeY/2)
        {
            q3++;
        }
        else if(posX >(int)sizeX/2 && posY >(int)sizeY/2)
        {
            q4++;
        }
        
        return (posX,posY);
    }

    public (int,int) StepPerSecond(int posX, int posY, int vX, int vY){
        int newPosX = posX + vX;
        int newPosY = posY + vY;
        
        if(newPosX >= sizeX){
            newPosX = newPosX - sizeX;
        }
        if(newPosX < 0){
            newPosX = sizeX + newPosX;
        }
        if(newPosY >= sizeY){
            newPosY = newPosY - sizeY;
        }
        if(newPosY < 0){
            newPosY = sizeY + newPosY;
        }
        //Console.WriteLine(newPosX+" "+newPosY);
        return (newPosX,newPosY);
    }


}
