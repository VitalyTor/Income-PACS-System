using System;

namespace InCome
{
	/// <summary>
	/// Класс для перекодирования строки с цифровой последовательностью в строку символов, необходимую для отображения штрих-кода (Code 128) шрифтом barcode.ttf  
	/// </summary>
	public class CEncode128
	{
		
		private static string [] ar=new string[107]{"212222","222122","222221","121223","121322","131222","122213",
				"122312","132212","221213","221312","231212","112232","122132","122231","113222","123122","123221",
				"223211","221132","221231","213212","223112","312131","311222","321122","321221","312212","322112",
				"322211","212123","212321","232121","111323","131123","131321","112313","132113","132311","211313",
				"231113","231311","112133","112331","132131","113123","113321","133121","313121","211331","231131",
				"213113","213311","213131","311123","311321","331121","312113","312311","332111","314111","221411",
				"431111","111224","111422","121124","121421","141122","141221","112214","112412","122114","122411",
				"142112","142211","141211","221114","413111","241112","134111","111242","121142","121241","114212","124112",
				"124211","411212","421112","421211","212141","214121","412121","111143","111341","131141","114113",
				"114311","411113","411311","113141","114131","311141","411131","211412","211214","211232","2331112"};
		
		/// <summary>
		/// Перекодирует строку
		/// </summary>
		/// <param name="A">Строка из последовательности цифр</param>
		/// <returns>Закодированная строка</returns>
		public static string Encode128(string A)
		{
			int [] bCode=new int[1024];
			int bInd;
			string curMode;
			int ch,ch2;
			int i,lenA,cCode;
			string s;
		

			//собираем строку кодов
			bInd=0;
			curMode="";
			i=0;
			lenA=A.Length;
			while (i<lenA)
			{
				//текущий символ в строке
				ch=A[i];
				++i;
				//разбираются символы от 0 до 127
				if(ch<=127)
				{
					//следующий символ
					if(i<lenA) ch2=A[i];
					else ch2=0;
					//пара цифр - режим С
					if((ch>=48) && (ch<=57) && (ch2>=48) && (ch2<=58))
					{
						++i;
						if(bInd==0)
						{//начало c режима С
							curMode="C";
							bCode[bInd]=105;
							++bInd;
						}
						else
						if(curMode!="C")
						{//переключится на режим С
							curMode="C";
							bCode[bInd]=99;
							++bInd;
						}
						//добавить символ режима С
						
						bCode[bInd]=Convert.ToInt32((char)ch+""+(char)ch2);
						++bInd;
					}
					else
					{
						if(bInd==0)
						{
							if(ch<32)
							{	//начало c режима А
								curMode="A";
								bCode[bInd]=103;
								++bInd;
							}
							else
							{	//начало c режима B
								curMode="B";
								bCode[bInd]=104;
								++bInd;
							}
						}
						//переключение по надобности в режим А
						if((ch<32) && (curMode!="A"))
						{
							curMode="A";
							bCode[bInd]=101;
							++bInd;
						}
						//переключение по надобности в режим B
						if(((ch>=64) && (curMode!="B")) || (curMode=="C"))
						{
							curMode="B";
							bCode[bInd]=100;
							++bInd;
						}
						//служебные символы
						if(ch<32)
						{
							bCode[bInd]=ch+64;
							++bInd;
						}
						else //все другие символы
						{
							bCode[bInd]=ch-32;
							++bInd;
						}

					}

				}
			};
			//подсчитываем КС
			cCode=bCode[0];
			for(int j=1;j<bInd;j++)
				cCode=(cCode+bCode[j]*j) % 103;
			bCode[bInd]=cCode;
			++bInd;
			//завершающий символ
			bCode[bInd]=106;
			++bInd;
			//собираем строку символов шрифта
			s="";
			for(int j=0;j<bInd;j++)
				s=s+codeChar(code128ID(bCode[j]));
			return s;
		}
		
		private static string codeChar(string A)
		{
			string s="";		

			switch (A)
			{
				case "211412":s="A";
					break;
				case "211214":s="B";
					break;
				case "211232":s="C";
					break;
				case "2331112":s="@";
					break;
				default:s="";
					for(int j=0;j<=A.Length/2-1;j++)
						switch(A.Substring(2*j,2))
						{
							case "11":s=s+"0";
								break;
							case "21":s=s+"1";
								break;
							case "31":s=s+"2";
								break;
							case "41":s=s+"3";
								break;
							case "12":s=s+"4";
								break;
							case "22":s=s+"5";
								break;
							case "32":s=s+"6";
								break;
							case "42":s=s+"7";
								break;
							case "13":s=s+"8";
								break;
							case "23":s=s+"9";
								break;
							case "33":s=s+":";
								break;
							case "43":s=s+";";
								break;
							case "14":s=s+"<";
								break;
							case "24":s=s+"=";
								break;
							case "34":s=s+">";
								break;
							case "44":s=s+"?";
								break;
						}
					break;
			}

			return s;
		}
		private static string code128ID(int id)
		{
			return ar[id];;
		}

		public CEncode128()
		{			
		}

	}
}
