using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myconsolegame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Cardnumber(a, b);
            //카드: 1~10까지 2장씩 보유
            //카드패 : 중복 없이 1장 1장 분배
            //승판결 : 족보 return함수, 지금은 더했을때 높은게 win
            //배팅 : 카드분배 후, 2장 분배 후
            //순서 : 기본 배팅 > 카드분배 > 배팅 > 카드분배 > 승판결 //(반복) 

            int aimoney = 100000; //총 가지고있는돈 
            int mymoney = 100000;
            int totoalmoney = aimoney + mymoney;//게임전체 소지돈
            int basicbettingmoney = 1000;//기본배팅금, 다음배팅담는변수

            int totalbettingmoney = 0; //한 싸이클 총배팅돈 
            int aitotalbettingmoney = 0; //한 싸이클 배팅돈 담는 변수 (일시적)
            int mytotalbettingmoney = 0;

            int bettinggap = 0;

            int[] totalCard = new int[20];//총카드 숫자에대한 배열

            int aiCard1 = 1;//ai카드패
            int aiCard2 = 2;

            int myCard1 = 3;//나의패
            int myCard2 = 4;

            int adrand1;//카드셔플 랜덤 순간만사용temp같은애
            int adrand2;


            int winpoint = 0;//승판결 나중에 draw발동시

            string bettinganswer = "하프"; //이름
            int airandombetting = 2;//ai가 몰 배팅하는지


            int aipoint = aiCard1 + aiCard2;//임시용 승 포인트
            int mypoint = myCard1 + myCard2;

            int temp; //순간 교환용
            int shuffle = 20; //총카드 셔플 시행횟수//나중에는 값을올려야댐

            int stopanswer;//종료 선언

            //시간적 시각적
            Stopwatch watch = new Stopwatch();


            //랜덤 선언
            Random airandom1 = new Random();


            while (true)
            {
                //총카드 초기화
                for (int i = 0; i < 10; i++)
                {
                    totalCard[i] = i + 1;
                    //Console.Write(totalCard[i]);
                }
                for (int i = 10; i < 20; i++)
                {
                    totalCard[i] = -10 + i + 1;
                    // Console.Write(totalCard[i]);
                }

                //총카드 셔플 
                for (int i = 0; i < shuffle; i++)
                {

                    adrand1 = airandom1.Next(0, 20);
                    adrand2 = airandom1.Next(0, 20);
                    temp = totalCard[adrand1];
                    totalCard[adrand1] = totalCard[adrand2];
                    totalCard[adrand2] = temp;

                }


                airandombetting = airandom1.Next(1, 4);//1,2,3

                aiCard1 = totalCard[0];
                aiCard2 = totalCard[1];

                myCard1 = totalCard[2];
                myCard2 = totalCard[3];

                aipoint = aiCard1 + aiCard2;//임시 점수
                mypoint = myCard1 + myCard2;//임시 점수


                //기본금 배팅 시작
                Gamestartprint();
                stopanswer = int.Parse(Console.ReadLine());
                if (stopanswer == 0)
                {
                    break;
                }

                aimoney -= basicbettingmoney;
                aitotalbettingmoney += basicbettingmoney;

                mymoney -= basicbettingmoney;
                mytotalbettingmoney += basicbettingmoney;
                //판돈
                totalbettingmoney = mytotalbettingmoney + aitotalbettingmoney;
                Console.Clear();

                //ai,내가 기본금보다 돈이 적을때
                if (aimoney < basicbettingmoney)
                {
                    Console.WriteLine("기본배팅금 보다 돈이 없습니다.");
                    break;
                }


                //이미지 생성부분
                Console.WriteLine("AI");
                Console.WriteLine("aimoney : " + aimoney);
                Console.WriteLine();
                Console.WriteLine("=============================");
                Console.SetCursorPosition(8, 8);
                Console.WriteLine(">ai카드");
                Console.SetCursorPosition(8, 13);
                Console.Write("판돈 : $" + totalbettingmoney);
                Console.SetCursorPosition(8, 18);
                Console.WriteLine(">player카드");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("=============================");
                Console.WriteLine("Player");
                Console.WriteLine("mymoney : " + mymoney);
                //카드 1장 분배
                Timedelay(0.5);
                AiCardnumber1(11);//ai이미지
                //AiCardnumber2(aiCard2);//my이미지
                Timedelay(0.5);
                MyCardnumber1(myCard1);
                // MyCardnumber2(myCard2);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();



                Timedelay(1);
                //카드1장 분배 후 배팅
                Console.WriteLine("배팅 뭐 할래? (1. 하프,2. 땡,3. 다이)");
                //나의 배팅 1,2,3 입력 후 금액 변경
                bettinganswer = Console.ReadLine();

                Console.Clear();
                if (aimoney == 0)
                {
                    Console.WriteLine("콜");
                    if (mymoney <= mytotalbettingmoney)
                    {
                        mytotalbettingmoney = mytotalbettingmoney + mymoney;
                        mymoney = 0;
                    }
                    else if (mymoney > mytotalbettingmoney)
                    {
                        if (basicbettingmoney <= mymoney)
                        {
                            mymoney -= basicbettingmoney;
                            mytotalbettingmoney += basicbettingmoney;
                        }
                        else if (basicbettingmoney > mymoney)
                        {
                            //배팅금차이
                            bettinggap = 0;
                            bettinggap = basicbettingmoney - mymoney;
                            basicbettingmoney = basicbettingmoney - bettinggap;
                            mytotalbettingmoney = mytotalbettingmoney + mymoney;
                            mymoney = 0;
                        }



                    }

                }
                else if (aimoney > 0)
                {
                    //배팅금보다 내돈이 적을때 if()???
                    if (mymoney <= mytotalbettingmoney)
                    {
                        mytotalbettingmoney = mytotalbettingmoney + mymoney;
                        mymoney = 0;
                    }
                    else if (mymoney > mytotalbettingmoney)
                    {
                        if (bettinganswer == "1")
                        {
                            if (basicbettingmoney * 3 <= mymoney)
                            {
                                mymoney -= basicbettingmoney * 3;
                                mytotalbettingmoney += basicbettingmoney * 3;
                                basicbettingmoney = basicbettingmoney * 3;
                            }
                            else if (basicbettingmoney * 3 > mymoney)
                            {
                                //배팅금차이
                                bettinggap = 0;
                                bettinggap = basicbettingmoney * 3 - mymoney;
                                basicbettingmoney = basicbettingmoney * 3 - bettinggap;
                                mytotalbettingmoney = mytotalbettingmoney + mymoney;
                                mymoney = 0;

                            }

                        }
                        else if (bettinganswer == "2")
                        {
                            if (basicbettingmoney * 2 <= mymoney)
                            {
                                mymoney -= basicbettingmoney * 2;
                                mytotalbettingmoney += basicbettingmoney * 2;
                                basicbettingmoney = basicbettingmoney * 2;
                            }
                            else if (basicbettingmoney * 2 > mymoney)
                            {
                                bettinggap = 0;
                                bettinggap = basicbettingmoney * 2 - mymoney;
                                basicbettingmoney = basicbettingmoney * 2 - bettinggap;
                                mytotalbettingmoney = mytotalbettingmoney + mymoney;
                                mymoney = 0;

                            }


                        }
                        else if (bettinganswer == "3")
                        {
                            Console.WriteLine("다이");
                        }

                    }
                }
                //ai배팅
                if (mymoney == 0)
                {
                    Console.WriteLine("콜");
                    //올인
                    if (aimoney <= aitotalbettingmoney)
                    {
                        aitotalbettingmoney = aitotalbettingmoney + aimoney;
                        aimoney = 0;
                    }
                    else if (aimoney > aitotalbettingmoney)
                    {
                        //ai배팅 1,2,3 랜덤 금액 변경

                        if (basicbettingmoney <= aimoney)
                        {
                            aimoney -= basicbettingmoney;
                            aitotalbettingmoney += basicbettingmoney;
                        }

                        else if (basicbettingmoney > aimoney)
                        {
                            //배팅금차이
                            bettinggap = 0;
                            bettinggap = basicbettingmoney - aimoney;
                            basicbettingmoney = basicbettingmoney - bettinggap;
                            aitotalbettingmoney = aitotalbettingmoney + aimoney;
                            aimoney = 0;
                        }
                    }
                }
                else if (mymoney > 0)
                {
                    if (aimoney <= aitotalbettingmoney)
                    {
                        aitotalbettingmoney = aitotalbettingmoney + aimoney;
                        aimoney = 0;
                    }
                    else if (aimoney > aitotalbettingmoney)
                    {
                        //ai배팅 1,2,3 랜덤 금액 변경
                        if (airandombetting == 1)
                        {
                            if (basicbettingmoney * 3 <= aimoney)
                            {
                                aimoney -= basicbettingmoney * 3;
                                aitotalbettingmoney += basicbettingmoney * 3;
                                basicbettingmoney = basicbettingmoney * 3;
                            }
                            else if (basicbettingmoney * 3 > aimoney)
                            {
                                //배팅금차이
                                bettinggap = 0;
                                bettinggap = basicbettingmoney * 3 - aimoney;
                                basicbettingmoney = basicbettingmoney * 3 - bettinggap;
                                aitotalbettingmoney = aitotalbettingmoney + aimoney;
                                aimoney = 0;
                            }
                        }
                        else if (airandombetting == 2)
                        {
                            if (basicbettingmoney * 2 <= aimoney)
                            {
                                aimoney -= basicbettingmoney * 2;
                                aitotalbettingmoney += basicbettingmoney * 2;
                                basicbettingmoney = basicbettingmoney * 2;
                            }
                            else if (basicbettingmoney * 2 > aimoney)
                            {
                                //배팅금차이
                                bettinggap = 0;
                                bettinggap = basicbettingmoney * 2 - aimoney;
                                basicbettingmoney = basicbettingmoney * 2 - bettinggap;
                                aitotalbettingmoney = aitotalbettingmoney + aimoney;
                                aimoney = 0;
                            }
                        }
                        else if (airandombetting == 3)
                        {
                            Console.WriteLine("다이");
                        }
                    }
                }








                //mytotalbettingmoney=0를 바꾸면댐
                totalbettingmoney = mytotalbettingmoney + aitotalbettingmoney;


                //이미지 생성부분
                Console.WriteLine("AI");
                Console.WriteLine("aimoney : " + aimoney);
                Console.WriteLine();
                Console.WriteLine("=============================");
                Console.SetCursorPosition(8, 8);
                Console.WriteLine(">ai카드");
                Console.SetCursorPosition(8, 13);
                Console.Write("판돈 : $" + totalbettingmoney);
                Console.SetCursorPosition(8, 18);
                Console.WriteLine(">player카드");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("=============================");
                Console.WriteLine("Player");
                Console.WriteLine("mymoney : " + mymoney);
                //카드 1장 분배
                AiCardnumber1(aiCard1);//ai이미지
                MyCardnumber1(myCard1);
                Timedelay(1);
                AiCardnumber2(11);//my이미지
                Timedelay(1);
                MyCardnumber2(myCard2);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();


                Timedelay(1);
                //카드2장 분배 후 배팅 시작
                Console.WriteLine("2배팅 뭐 할래? (1. 하프,2. 땡,3. 다이)");
                //Console.Clear();
                //나의 배팅 1,2,3 입력 후 금액 변경
                bettinganswer = Console.ReadLine();



                if (aimoney == 0)
                {
                    Console.WriteLine("콜");
                    if (mymoney <= mytotalbettingmoney)
                    {
                        mytotalbettingmoney = mytotalbettingmoney + mymoney;
                        mymoney = 0;
                    }
                    else if (mymoney > mytotalbettingmoney)
                    {
                        if (basicbettingmoney <= mymoney)
                        {
                            mymoney -= basicbettingmoney;
                            mytotalbettingmoney += basicbettingmoney;
                        }
                        else if (basicbettingmoney > mymoney)
                        {
                            //배팅금차이
                            bettinggap = 0;
                            bettinggap = basicbettingmoney - mymoney;
                            basicbettingmoney = basicbettingmoney - bettinggap;
                            mytotalbettingmoney = mytotalbettingmoney + mymoney;
                            mymoney = 0;
                        }



                    }

                }
                else if (aimoney > 0)
                {
                    //배팅금보다 내돈이 적을때 if()???
                    if (mymoney <= mytotalbettingmoney)
                    {
                        mytotalbettingmoney = mytotalbettingmoney + mymoney;
                        mymoney = 0;
                    }
                    else if (mymoney > mytotalbettingmoney)
                    {
                        if (bettinganswer == "1")
                        {
                            if (basicbettingmoney * 3 <= mymoney)
                            {
                                mymoney -= basicbettingmoney * 3;
                                mytotalbettingmoney += basicbettingmoney * 3;
                                basicbettingmoney = basicbettingmoney * 3;
                            }
                            else if (basicbettingmoney * 3 > mymoney)
                            {
                                //배팅금차이
                                bettinggap = 0;
                                bettinggap = basicbettingmoney * 3 - mymoney;
                                basicbettingmoney = basicbettingmoney * 3 - bettinggap;
                                mytotalbettingmoney = mytotalbettingmoney + mymoney;
                                mymoney = 0;

                            }

                        }
                        else if (bettinganswer == "2")
                        {
                            if (basicbettingmoney * 2 <= mymoney)
                            {
                                mymoney -= basicbettingmoney * 2;
                                mytotalbettingmoney += basicbettingmoney * 2;
                                basicbettingmoney = basicbettingmoney * 2;
                            }
                            else if (basicbettingmoney * 2 > mymoney)
                            {
                                bettinggap = 0;
                                bettinggap = basicbettingmoney * 2 - mymoney;
                                basicbettingmoney = basicbettingmoney * 2 - bettinggap;
                                mytotalbettingmoney = mytotalbettingmoney + mymoney;
                                mymoney = 0;

                            }


                        }
                        else if (bettinganswer == "3")
                        {
                            Console.WriteLine("다이");
                        }

                    }

                }







                if (mymoney == 0)
                {
                    Console.WriteLine("콜");
                    //올인
                    if (aimoney <= aitotalbettingmoney)
                    {
                        aitotalbettingmoney = aitotalbettingmoney + aimoney;
                        aimoney = 0;
                    }
                    else if (aimoney > aitotalbettingmoney)
                    {
                        //ai배팅 1,2,3 랜덤 금액 변경

                        if (basicbettingmoney <= aimoney)
                        {
                            aimoney -= basicbettingmoney;
                            aitotalbettingmoney += basicbettingmoney;
                        }

                        else if (basicbettingmoney > aimoney)
                        {
                            //배팅금차이
                            bettinggap = 0;
                            bettinggap = basicbettingmoney - aimoney;
                            basicbettingmoney = basicbettingmoney - bettinggap;
                            aitotalbettingmoney = aitotalbettingmoney + aimoney;
                            aimoney = 0;
                        }
                    }
                }
                else if (mymoney > 0)
                {
                    if (aimoney <= aitotalbettingmoney)
                    {
                        aitotalbettingmoney = aitotalbettingmoney + aimoney;
                        aimoney = 0;
                    }
                    else if (aimoney > aitotalbettingmoney)
                    {
                        //ai배팅 1,2,3 랜덤 금액 변경
                        if (airandombetting == 1)
                        {
                            if (basicbettingmoney * 3 <= aimoney)
                            {
                                aimoney -= basicbettingmoney * 3;
                                aitotalbettingmoney += basicbettingmoney * 3;
                                basicbettingmoney = basicbettingmoney * 3;
                            }
                            else if (basicbettingmoney * 3 > aimoney)
                            {
                                //배팅금차이
                                bettinggap = 0;
                                bettinggap = basicbettingmoney * 3 - aimoney;
                                basicbettingmoney = basicbettingmoney * 3 - bettinggap;
                                aitotalbettingmoney = aitotalbettingmoney + aimoney;
                                aimoney = 0;
                            }
                        }
                        else if (airandombetting == 2)
                        {
                            if (basicbettingmoney * 2 <= aimoney)
                            {
                                aimoney -= basicbettingmoney * 2;
                                aitotalbettingmoney += basicbettingmoney * 2;
                                basicbettingmoney = basicbettingmoney * 2;
                            }
                            else if (basicbettingmoney * 2 > aimoney)
                            {
                                //배팅금차이
                                bettinggap = 0;
                                bettinggap = basicbettingmoney * 2 - aimoney;
                                basicbettingmoney = basicbettingmoney * 2 - bettinggap;
                                aitotalbettingmoney = aitotalbettingmoney + aimoney;
                                aimoney = 0;
                            }
                        }
                        else if (airandombetting == 3)
                        {
                            Console.WriteLine("다이");
                        }
                    }
                }


                Console.Clear();


                //이미지
                totalbettingmoney = mytotalbettingmoney + aitotalbettingmoney;
                Console.WriteLine("AI");
                Console.WriteLine("aimoney : " + aimoney);
                Console.WriteLine();
                Console.WriteLine("=============================");
                Console.SetCursorPosition(8, 8);
                Console.WriteLine(">ai카드");
                Console.SetCursorPosition(8, 13);
                Console.Write("판돈 : $" + totalbettingmoney);
                Console.SetCursorPosition(8, 18);
                Console.WriteLine(">player카드");
                //카드 1장 분배
                AiCardnumber1(aiCard1);//ai이미지
                                       // AiCardnumber2(aiCard2);//my이미지
                MyCardnumber1(myCard1);
                // MyCardnumber2(myCard2);
                Console.WriteLine("=============================");
                Console.WriteLine("Player");
                Console.WriteLine("mymoney : " + mymoney);
                Console.WriteLine();
                Console.WriteLine();


                Console.Clear();

                //시간요소 넣기 ??
                //승판결 시작, 이기면 배팅금 가져감
                Console.WriteLine("승 판결 시작");

                //시간딜레이
                Timedelay(1);

                if (aipoint > mypoint)
                {
                    aimoney = aimoney + totalbettingmoney;
                    Console.WriteLine("ai win");

                }
                else if (aipoint < mypoint)
                {
                    mymoney = mymoney + totalbettingmoney;
                    Console.WriteLine("player win");


                }
                else if (aipoint == mypoint)
                {
                    Console.WriteLine("draw");
                }


                Console.WriteLine("AI");
                Console.WriteLine("aimoney : " + aimoney);
                Console.WriteLine();
                Console.WriteLine("=============================");
                Console.SetCursorPosition(8, 8);
                Console.WriteLine(">ai카드");
                Console.SetCursorPosition(8, 13);
                Console.Write("판돈 : $" + totalbettingmoney + " 이었음");
                Console.SetCursorPosition(8, 18);
                Console.WriteLine(">player카드");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("=============================");
                Console.WriteLine("Player");
                Console.WriteLine("mymoney : " + mymoney);

                //이긴 카드만 시각화
                if (aipoint > mypoint)
                {
                    AiCardnumber1(aiCard1);//ai이미지
                    AiCardnumber2(aiCard2);//my이미지
                    Console.SetCursorPosition(0, 22);
                }
                else if (aipoint < mypoint)
                {
                    MyCardnumber1(myCard1);
                    MyCardnumber2(myCard2);
                    Console.SetCursorPosition(0, 22);
                }
                else if (aipoint == mypoint)
                {
                }

                //Console.Clear();
                //배팅금액0인경우
                if (mymoney <= 0)
                {
                    mymoney = 0;
                    aimoney = totoalmoney;
                    Console.WriteLine("ai한테 다 털림");
                    Console.WriteLine("aimoney : " + aimoney);
                    Console.WriteLine("mymoney : " + mymoney);
                    break;
                }
                else if (aimoney <= 0)
                {
                    aimoney = 0;
                    mymoney = totoalmoney;
                    Console.WriteLine("ai돈 털었음");
                    Console.WriteLine("aimoney : " + aimoney);
                    Console.WriteLine("mymoney : " + mymoney);
                    break;
                }

                // if(draw, 다시하기 아닌경우)??
                mytotalbettingmoney = 0;
                aitotalbettingmoney = 0;
                totalbettingmoney = mytotalbettingmoney + aitotalbettingmoney;
                basicbettingmoney = 1000;
                Timedelay(6);
                Console.Clear();

            }

        }

        static void Timedelay(double second)
        {

            Stopwatch watch = new Stopwatch();
            //시간=0
            watch.Start();
            //시간적 여유
            while (true)
            {
                if (watch.ElapsedMilliseconds >= 1000 * second)
                {
                    watch.Restart();
                    break;
                }
            }

        }
        static void Gamestartprint()
        {
            Console.WriteLine("=============================");
            Console.WriteLine("게임 시작(임시용 아무거나 누르셈)");
            Console.WriteLine("기본배팅 원하면 아무거나 누르셈");
            Console.WriteLine("종료 : 0번");
            Console.WriteLine("=============================");

        }



        static void AiCardnumber1(int firstcard)
        {

            string[,,] str = new string[10, 7, 7]
            {
                //1
                {
                {"■","■","■","■","■","■","■"},
                {"■","■","■","　","■","■","■"},
                {"■","■","　","　","■","■","■"},
                {"■","■","■","　","■","■","■"},
                {"■","■","■","　","■","■","■"},
                {"■","■","　","　","　","■","■"},
                {"■","■","■","■","■","■","■"},
                },

                 //2
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","　","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","■","■"},
                },

                //3
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","■","■"},
                },

                 //4
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","■","■","■","　","■"},
                {"■","　","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","■","■","■","■","■","■"},
                },

                //5
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","　","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","■","■"},
                },

                 //6
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","　","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","　","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","■","■"},
                },

                //7
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","　","■","■","■","　","■"},
                {"■","　","■","■","■","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","■","■","■","■","■","■"},
                },

                 //8
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","　","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","　","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","■","■"},
                },

                //9
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","　","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","■","■","■","■","■","■"},
                },

                //10
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","■","　","　","　","■"},
                {"■","　","■","　","■","　","■"},
                {"■","　","■","　","■","　","■"},
                {"■","　","■","　","■","　","■"},
                {"■","　","■","　","　","　","■"},
                {"■","■","■","■","■","■","■"},
                },


            };

            if (firstcard == 11)
            {
                for (int j = 0; j < 7; j++)
                {
                    for (int k = 0; k < 7; k++)
                    {
                        Console.SetCursorPosition(33 + 2 * k, 5 + j);
                        Console.Write("■");


                    }

                    Console.WriteLine();

                }

            }
            else
            {
                for (int j = 0; j < 7; j++)
                {
                    for (int k = 0; k < 7; k++)
                    {
                        Console.SetCursorPosition(33 + 2 * k, 5 + j);
                        Console.Write(str[firstcard - 1, j, k]);


                    }

                    Console.WriteLine();

                }
            }

            //Console.SetCursorPosition(3, 5);
            //Console.BackgroundColor = ConsoleColor.Red;
            //Console.WriteLine("■");
            //Console.ResetColor();


        }

        static void AiCardnumber2(int secondcard)
        {
            string[,,] str = new string[10, 7, 7]
             {
                //1
                {
                {"■","■","■","■","■","■","■"},
                {"■","■","■","　","■","■","■"},
                {"■","■","　","　","■","■","■"},
                {"■","■","■","　","■","■","■"},
                {"■","■","■","　","■","■","■"},
                {"■","■","　","　","　","■","■"},
                {"■","■","■","■","■","■","■"},
                },

                 //2
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","　","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","■","■"},
                },

                //3
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","■","■"},
                },

                 //4
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","■","■","■","　","■"},
                {"■","　","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","■","■","■","■","■","■"},
                },

                //5
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","　","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","■","■"},
                },

                 //6
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","　","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","　","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","■","■"},
                },

                //7
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","　","■","■","■","　","■"},
                {"■","　","■","■","■","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","■","■","■","■","■","■"},
                },

                 //8
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","　","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","　","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","■","■"},
                },

                //9
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","　","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","■","■","■","■","■","■"},
                },

                //10
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","■","　","　","　","■"},
                {"■","　","■","　","■","　","■"},
                {"■","　","■","　","■","　","■"},
                {"■","　","■","　","■","　","■"},
                {"■","　","■","　","　","　","■"},
                {"■","■","■","■","■","■","■"},
                },


             };

            if (secondcard == 11)
            {
                for (int j = 0; j < 7; j++)
                {
                    for (int k = 0; k < 7; k++)
                    {
                        Console.SetCursorPosition(55 + 2 * k, 5 + j);
                        Console.Write("■");


                    }

                    Console.WriteLine();

                }

            }
            else
            {

                for (int j = 0; j < 7; j++)
                {
                    for (int k = 0; k < 7; k++)
                    {
                        Console.SetCursorPosition(55 + 2 * k, 5 + j);
                        Console.Write(str[secondcard - 1, j, k]);

                    }

                    Console.WriteLine();

                }
            }
            //Console.SetCursorPosition(22, 5);
            //Console.BackgroundColor = ConsoleColor.Red;
            //Console.WriteLine("■");
            //Console.ResetColor();
            //Console.SetCursorPosition(0, 13);
        }




        static void MyCardnumber1(int firstcard)
        {
            string[,,] str = new string[10, 7, 7]
           {
                //1
                {
                {"■","■","■","■","■","■","■"},
                {"■","■","■","　","■","■","■"},
                {"■","■","　","　","■","■","■"},
                {"■","■","■","　","■","■","■"},
                {"■","■","■","　","■","■","■"},
                {"■","■","　","　","　","■","■"},
                {"■","■","■","■","■","■","■"},
                },

                 //2
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","　","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","■","■"},
                },

                //3
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","■","■"},
                },

                 //4
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","■","■","■","　","■"},
                {"■","　","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","■","■","■","■","■","■"},
                },

                //5
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","　","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","■","■"},
                },

                 //6
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","　","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","　","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","■","■"},
                },

                //7
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","　","■","■","■","　","■"},
                {"■","　","■","■","■","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","■","■","■","■","■","■"},
                },

                 //8
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","　","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","　","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","■","■"},
                },

                //9
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","　","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","■","■","■","■","■","■"},
                },

                //10
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","■","　","　","　","■"},
                {"■","　","■","　","■","　","■"},
                {"■","　","■","　","■","　","■"},
                {"■","　","■","　","■","　","■"},
                {"■","　","■","　","　","　","■"},
                {"■","■","■","■","■","■","■"},
                },


           };
            //나오는수


            for (int j = 0; j < 7; j++)
            {
                for (int k = 0; k < 7; k++)
                {
                    Console.SetCursorPosition(33 + 2 * k, 15 + j);
                    Console.Write(str[firstcard - 1, j, k]);


                }
                Console.WriteLine();
            }
            //Console.SetCursorPosition(0, 14);//ai, my 시각 경계선
            //Console.WriteLine("==============================================");
            //Console.SetCursorPosition(3, 15);
            //Console.BackgroundColor = ConsoleColor.Red;
            //Console.WriteLine("■");
            //Console.ResetColor();


        }
        static void MyCardnumber2(int secondcard)
        {
            string[,,] str = new string[10, 7, 7]
           {
                //1
                {
                {"■","■","■","■","■","■","■"},
                {"■","■","■","　","■","■","■"},
                {"■","■","　","　","■","■","■"},
                {"■","■","■","　","■","■","■"},
                {"■","■","■","　","■","■","■"},
                {"■","■","　","　","　","■","■"},
                {"■","■","■","■","■","■","■"},
                },

                 //2
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","　","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","■","■"},
                },

                //3
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","■","■"},
                },

                 //4
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","■","■","■","　","■"},
                {"■","　","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","■","■","■","■","■","■"},
                },

                //5
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","　","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","■","■"},
                },

                 //6
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","　","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","　","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","■","■"},
                },

                //7
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","　","■","■","■","　","■"},
                {"■","　","■","■","■","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","■","■","■","■","■","■"},
                },

                 //8
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","　","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","　","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","■","■"},
                },

                //9
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","　","　","　","　","■"},
                {"■","　","■","■","■","　","■"},
                {"■","　","　","　","　","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","■","■","■","■","　","■"},
                {"■","■","■","■","■","■","■"},
                },

                //10
                {
                {"■","■","■","■","■","■","■"},
                {"■","　","■","　","　","　","■"},
                {"■","　","■","　","■","　","■"},
                {"■","　","■","　","■","　","■"},
                {"■","　","■","　","■","　","■"},
                {"■","　","■","　","　","　","■"},
                {"■","■","■","■","■","■","■"},
                },


           };

            for (int j = 0; j < 7; j++)
            {
                for (int k = 0; k < 7; k++)
                {
                    Console.SetCursorPosition(55 + 2 * k, 15 + j);
                    Console.Write(str[secondcard - 1, j, k]);

                }

                Console.WriteLine();

            }
            //Console.SetCursorPosition(22, 15);
            //Console.BackgroundColor = ConsoleColor.Red;
            //Console.WriteLine("■");
            //Console.ResetColor();
            //Console.SetCursorPosition(0, 25);

        }

    }
}