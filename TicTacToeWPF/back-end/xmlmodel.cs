using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using TicTacToe_WPF.middleware_controller;
using System.IO;

namespace TicTacToe_WPF.back_end
{
    public class XmlModel
    {
        private static XElement gamexml;

        public static void CreateGameXml()
        {
            if (File.Exists("game.xml"))
            {
                File.Delete("game.xml");
            }
            gamexml = new XElement("Game");
            XElement xmlnode = new XElement("Opp"); xmlnode.Value = (GameController.opponent_player_mode == Constants.COMPUTER ? "computer" : "user");
            gamexml.AddFirst(xmlnode);

            XElement elementTurn = new XElement("Turn");
            elementTurn.SetValue(GameController.TURN);
            gamexml.Add(elementTurn);

            xmlnode = new XElement("Move");
            gamexml.Add(xmlnode);
            return;
        }
        public static void AddActionXml(string pos, string sym, int id, string type)
        {
            if (gamexml == null) CreateGameXml();

            gamexml.Element("Turn").SetValue(GameController.TURN);

            XElement xStep = new XElement("Step");
            xStep.SetAttributeValue("id", id.ToString());
            xStep.SetAttributeValue("time", Environment.TickCount.ToString());

            XElement xPlayer = new XElement("Player");
            xPlayer.SetAttributeValue("id", "10");
            xPlayer.SetAttributeValue("type", type);
            xStep.Add(xPlayer);

            XElement xPlay = new XElement("Play");
            xPlay.SetAttributeValue("sign", sym);
            xPlay.SetValue(pos);
            xStep.Add(xPlay);

            gamexml.Element("Move").Add(xStep);
            gamexml.Save("game.xml");
            return;
        }
        public static void LoadGameXml(string filename)
        {
            XElement resultsXml = XElement.Load(filename);

            if (resultsXml.Name.LocalName != "Game")
            {
                MessageBox.Show("could not resume game, because selected game.xml is invalid");
                return;
            }

            XElement node = (XElement)resultsXml.FirstNode;
            do
            {
                switch (node.Name.LocalName)
                {
                    case "Opp":
                        GameController.start_mode = Constants.PEER_TO_PEER;
                        GameController.TURN = true;
                        if (node.Value == "computer")
                        {
                            GameController.start_mode = Constants.PEER_TO_COMPUTER;
                        }
                        break;
                    case "Turn":
                        GameController.TURN = bool.Parse(node.Value);
                        break;
                    case "Move":
                        XElement stepnode = (XElement)node.FirstNode;

                        bool bFirst = true;
                        do
                        {
                            GameController.index = int.Parse(((XElement)stepnode).Attribute("id").Value.ToString());
                            foreach (XNode stepinfo in stepnode.Nodes())
                            {
                                if (((XElement)stepinfo).Name.LocalName == "Play")
                                {
                                    if (bFirst)
                                    {
                                        bFirst = false;
                                        if(((XElement)stepnode).Element("Player").Attribute("type").Value.ToString() == "computer")
                                        {
                                            if (((XElement)stepinfo).Attribute("sign").Value == Constants.X_SYMBOL)
                                            {
                                                ArtificialIntelligence.AI_SYMBOL = Constants.X_SYMBOL;
                                                ArtificialIntelligence.ENEMY_SYMBOL = Constants.O_SYMBOL;

                                                GameController.Player1_Symbol = Constants.O_SYMBOL;
                                                GameController.Player2_Symbol = Constants.X_SYMBOL;
                                            }
                                            else
                                            {
                                                ArtificialIntelligence.AI_SYMBOL = Constants.O_SYMBOL;
                                                ArtificialIntelligence.ENEMY_SYMBOL = Constants.X_SYMBOL;

                                                GameController.Player1_Symbol = Constants.X_SYMBOL;
                                                GameController.Player2_Symbol = Constants.O_SYMBOL;
                                            }
                                        }
                                        else
                                        {
                                            if (((XElement)stepinfo).Attribute("sign").Value == Constants.X_SYMBOL)
                                            {
                                                ArtificialIntelligence.AI_SYMBOL = Constants.O_SYMBOL;
                                                ArtificialIntelligence.ENEMY_SYMBOL = Constants.X_SYMBOL;

                                                GameController.Player1_Symbol = Constants.X_SYMBOL;
                                                GameController.Player2_Symbol = Constants.O_SYMBOL;
                                            }
                                            else
                                            {
                                                ArtificialIntelligence.AI_SYMBOL = Constants.X_SYMBOL;
                                                ArtificialIntelligence.ENEMY_SYMBOL = Constants.O_SYMBOL;

                                                GameController.Player1_Symbol = Constants.O_SYMBOL;
                                                GameController.Player2_Symbol = Constants.X_SYMBOL;
                                            }
                                        }
                                    }

                                    string pos = ((XElement)stepinfo).Value.ToString().Trim();

                                    GameController.RestoreStep(pos, ((XElement)stepinfo).Attribute("sign").Value.ToString());
                                    //Button btn = GameController.GetButton(pos);
                                    //if (btn != null)
                                    //{
                                    //    gameAction_Click(btn, null);
                                    //}
                                }

                                //if (((XElement)stepinfo).Attribute("type") != null)
                                //{
                                //    if (((XElement)stepinfo).Attribute("type").Value == "computer")
                                //    {
                                //        GameController.start_mode = Constants.PEER_TO_COMPUTER;
                                //        break;
                                //    }
                                //}
                            }
                        }
                        while ((stepnode = (XElement)stepnode.NextNode) != null);
                        break;
                }
                //System.Diagnostics.Debug.WriteLine(node.ToString());
            }
            while ((node = (XElement)node.NextNode) != null);
            //String xmlFileContents = File.ReadAllText(openFileDialog.FileName);
            //using (XmlReader reader = XmlReader.Create(new StringReader(xmlFileContents)))
            //{ 
            //}

            gamexml = (XElement)resultsXml;

            GameController.checkGameStatus();
        }
    }
}
