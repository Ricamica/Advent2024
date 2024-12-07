
using System.Security.Principal;

string[] input = System.IO.File.ReadAllLines(@"Tinput.txt");
string productOptions = "+*";
long pocetZnaminek = productOptions.Length;
List<long> resultList = [];

foreach(var radek in input){
    long DesiredResult  = long.Parse(radek.Split(": ")[0]);
    string[] cisla = radek.Split(": ")[1].Split(" ");
    long PocetMezer =GetPocetMezer(radek.Split(": ")[1]);
    long PocetKombinaci = (long)Math.Pow(pocetZnaminek,PocetMezer);
    
    List<string> KombinaceZnamenek= new List<string>();
    BuildPossibleCombination(0, new List < string >(),PocetMezer,productOptions,ref KombinaceZnamenek);
    foreach(var jednaKombinace in KombinaceZnamenek){
//Console.WriteLine(jednaKombinace);
        long vysledek = long.Parse(cisla[0]);
            
        for(long index = 0 ; index<jednaKombinace.Length;index++){
            
            if(jednaKombinace[(int)index].ToString() == "+"){
                vysledek+= long.Parse(cisla[(int)index+1]);
            }
            if(jednaKombinace[(int)index].ToString() == "*"){
                vysledek*= long.Parse(cisla[(int)index+1]);
            }
            
            
        }
        if(vysledek == DesiredResult){
            if(!resultList.Contains(DesiredResult)){
                resultList.Add(DesiredResult);
            }
        }
    }
}
long result = 0;
foreach(var i in resultList){
    result+= i;
}
Console.WriteLine(result);


string GetValue(string line){
    return line.Split(": ")[0];
}

long GetPocetMezer(string line){
    long pocet = 0;
    foreach(char c in line){
        if(c.ToString() ==" "){
            pocet++;
        }   
    }
    return pocet;
}

static void BuildPossibleCombination(long level, List < string > output,long PocetKombinaci, string ListZnaminek, ref List<string> result) {    
    if(level < PocetKombinaci){
        foreach(var znaminko in ListZnaminek){
            List < string > resultList = new List < string > ();
            resultList.AddRange(output);
            resultList.Add(znaminko.ToString());
            if (resultList.Count == PocetKombinaci) {
                result.Add((string.Join("", resultList)));
            }
            BuildPossibleCombination(level+1, resultList, PocetKombinaci, ListZnaminek,ref result);
        }
    }
}
