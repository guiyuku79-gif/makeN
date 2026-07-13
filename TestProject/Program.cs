//.Net SDKを使用したC#の実行
using System.Collections.Generic;
using System.Net.Quic;


Question question = new Question();
question.DecideQuestion();

public class Question
{

    public void DecideQuestion()
    {
        List<Fraction> numbers = new List<Fraction>{};
        for(int i = 0;i < 4; i++)
        {
            numbers.Add(new Fraction(Random.Range(1,10),1));
        }

        //計算結果を保存
        List<int> sum = new List<int>{};
        for(int i= 0;i < 20;i++) sum.Add(0);

        for(int o1 = 0;o1 < 6; o1++)
        {
            for(int o2 = 0;o2 < 6; o2++)
            {
                for(int o3 = 0;o3 < 6; o3++)
                {
                    NumberCombination(o1,o2,o3,sum,numbers);
                }
            }
        }

        Console.WriteLine(sum);
    }

    private void NumberCombination(int o1,int o2,int o3,List<int> sum,List<Fraction> numbers)
    {
        
    }


}




