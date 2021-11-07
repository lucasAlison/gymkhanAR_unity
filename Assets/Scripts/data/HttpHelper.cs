using System;
using System.IO;
using System.Net;
using UnityEngine;

namespace DataHelpers
{
	/*
	 *	Class used for connection with backend
	 *  Here you should rewrite the methods for connect with your backend
	 */
    public class HttpHelper
    {

		public static Level getLevel(string token)
        {
			Level level = null;
			var requisicaoWeb = WebRequest.CreateHttp("http://167.99.237.34/unity/level/"+token);
			requisicaoWeb.Method = "GET";
			using (var resposta = requisicaoWeb.GetResponse())
			{
				var streamDados = resposta.GetResponseStream();
				StreamReader reader = new StreamReader(streamDados);
				object objResponse = reader.ReadToEnd();
				level = Level.fromJSON(objResponse.ToString());
				streamDados.Close();
				resposta.Close();
			}
			return level;
			//if(Mockup.levelPointer >= Mockup.levels.Length) Mockup.levelPointer = 0;
			//return Mockup.levels[Mockup.levelPointer++];	
		}


		public static Tip getTip(string token, string id)
        {
			Tip tip = null;
			Debug.Log(id);
			var requisicaoWeb = WebRequest.CreateHttp("http://167.99.237.34/unity/tip/"+id);
			requisicaoWeb.Method = "GET";
			using (var resposta = requisicaoWeb.GetResponse())
			{
				var streamDados = resposta.GetResponseStream();
				StreamReader reader = new StreamReader(streamDados);
				object objResponse = reader.ReadToEnd();
				tip = Tip.fromJSON(objResponse.ToString());
				streamDados.Close();
				resposta.Close();
			}
			return tip;
			//return Mockup.tips[id];
		}

		public static Tracking getTracking(string token, string id)
		{
			Tracking tracking = null;
			var requisicaoWeb = WebRequest.CreateHttp("http://167.99.237.34/unity/tracking/"+id);
			requisicaoWeb.Method = "GET";
			using (var resposta = requisicaoWeb.GetResponse())
			{
				var streamDados = resposta.GetResponseStream();
				StreamReader reader = new StreamReader(streamDados);
				object objResponse = reader.ReadToEnd();
				tracking = Tracking.fromJSON(objResponse.ToString());
				streamDados.Close();
				resposta.Close();
			}			
			return tracking;
			//return Mockup.trackings[id];
		}

		public static Question getQuestion(string token, string id)
		{
			Question question = null;
			var requisicaoWeb = WebRequest.CreateHttp("http://167.99.237.34/unity/question/"+id);
			requisicaoWeb.Method = "GET";
			using (var resposta = requisicaoWeb.GetResponse())
			{
				var streamDados = resposta.GetResponseStream();
				StreamReader reader = new StreamReader(streamDados);
				object objResponse = reader.ReadToEnd();								
				question = Question.fromJSON(objResponse.ToString());
				streamDados.Close();
				resposta.Close();
			}
			return question;
			//return Mockup.questions[id];
		}

	}


	/*
	 *  Mockup class used for test
	 *  Delete this class if you use http request
	 */
	public class Mockup
	{
		public static int levelPointer = 0;

		public static Level[] levels = new Level[]{
			Level.fromJSON("{"+
                           "\"id\": 0,"+
                           "\"levelName\":\"Level 1\","+
						   "\"sceneSequence\":[\"Tip\",\"Tracking\",\"Question\"],"+
                           "\"tipId\": 0,"+
                           "\"questionId\": 0,"+
                           "\"trackingId\": 0"+
                           "}"
			)
		};

		public static Tip[] tips = new Tip[]{
			Tip.fromJSON("{" +
                         "\"id\": 0," +
                         "\"title\": \"Grave a Dica\"," +
                         "\"subtitle\": \"Procure por imagens de um baú\"," + 
                         "\"body\": \"Sou uma estrutura de repetição. Quem sou eu?\""+
                         "}"
			)
		};

		public static Tracking[] trackings = new Tracking[]{
			Tracking.fromJSON("{" +
                              "\"id\": 0," +
							  "\"type\": \"image\"," +
							  "\"url\": \"http://167.99.237.34/files/chest.wtc\"," +
                              "\"assetName\": \"Cube\"" + 
                              "}"
			)
		};

		public static Question[] questions = new Question[]{
			Question.fromJSON("{" +
							  "\"id\": 0," +
							  "\"text\": \"Sou utilizado quando você precisa repetir várias vezes um ou mais comandos. " +
									      "Dependendo da situação, posso não ser executado. "+
									      "Não sei quantas repetições serão executadas.\"," +
							  "\"options\": [" +
							     "{" +
									 "\"text\": \"Para...faça (for)\","+
									 "\"correction\": \"Ops. A estrutura para...faça (for) tem comportamento de repetição de "+
													  "um bloco de sentenças por um número específico de interações. "+
													  "Um para...faça sempre está acompanhado de uma variável contadora que "+
													  "armazena quantas vezes o bloco de sentenças da estrutura de repetição "+
													  "deve ser executada.\","+
									 "\"isCorrect\": false" +
								 "}," +
								 "{" +
									 "\"text\": \"Enquanto...faça (while)\","+
									 "\"correction\": \"Parabéns! Você acertou! O enquanto...faça (while) é a estrutura de "+
												      "repetição mais simples. Ele repete a execução de um bloco de sentenças "+
													  "enquanto uma condição permanecer verdadeira. Na primeira vez que a "+
													  "condição se tornar falsa, o while não repetirá a execução do bloco. "+
													  "Pode acontecer de não ser executada.\","+
									 "\"isCorrect\": true" +
								 "}," +
								 "{" +
									 "\"text\": \"Faça...enquanto (do while)\","+
									 "\"correction\": \"Ops! A estrutura faça...enquanto (do while) tem o comportamento de "+
													  "repetição de comandos, porém a condição é verificada após executar o "+
													  "bloco de instruções correspondente. Ou seja, é necessário rodar os "+
													  "comandos pelo menos uma vez para realizar a verificação da condição.\","+
									 "\"isCorrect\": false" +
								 "}" +
							   "]" +
							   "}"
			)
		};
	}   
}
