using F1Tippspiel.Db.Authentication;
using F1Tippspiel.Db.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace F1Tippspiel.Api.Processors
{
	public class PlayerPointsProcessor
	{

		/// <summary>
		/// Berechnet die Punkte die ein Spieler mit dieser Wette erzielt hat
		/// </summary>
		/// <param name="bet">Die Referenz auf die Wette zu der die Punkte berechnet werden sollen</param>
		/// <returns>Die berechneten Punkte für diese Wette</returns>
		public static int GetPointsForBet(RaceBet bet)
		{
			int punkte = 0;

			try
			{
				var orderedRaceResult = bet.Race.Results.OrderBy(r => r.Position).ToList();
				var orderedQualifyingResult = bet.Race.QualifyingResults.OrderBy(r => r.Position).ToList();

				int platz1Id = orderedRaceResult[0].Driver.DriverId;
				int platz2Id = orderedRaceResult[1].Driver.DriverId;
				int platz3Id = orderedRaceResult[2].Driver.DriverId;

				if (bet.Place1.DriverId == platz1Id)
				{
					//Richtig geraten = 5 Punkte
					punkte += 5;

					//Bonuspunkte berechnen
					var driver = orderedQualifyingResult.FirstOrDefault(d => d.Driver.DriverId == bet.Place1.DriverId);
					punkte += CalculateBonusPoints(driver == null ? 8 : driver.Position);
				}
				else
				{
					//Es kann sein dass der Tipp auf dem zweiten oder dritten Platz gelandet ist
					if (bet.Place1.DriverId == platz2Id || bet.Place1.DriverId == platz3Id)
					{
						//Der getippte Fahrer ist immerhin unter den Top 3 = 1 Punkt
						punkte += 1;

						//Bonuspunkte berechnen
						var driver = orderedQualifyingResult.FirstOrDefault(d => d.Driver.DriverId == bet.Place1.DriverId);
						punkte += CalculateBonusPoints(driver == null ? 8 : driver.Position);
					}
				}

				if (bet.Place2.DriverId == platz2Id)
				{
					//Richtig geraten = 5 Punkte
					punkte += 5;

					//Bonuspunkte berechnen
					var driver = orderedQualifyingResult.FirstOrDefault(d => d.Driver.DriverId == bet.Place2.DriverId);
					punkte += CalculateBonusPoints(driver == null ? 8 : driver.Position);
				}
				else
				{
					//Es kann sein dass der Tipp auf dem zweiten oder dritten Platz gelandet ist
					if (bet.Place2.DriverId == platz1Id || bet.Place2.DriverId == platz3Id)
					{
						//Der getippte Fahrer ist immerhin unter den Top 3 = 1 Punkt
						punkte += 1;

						//Bonuspunkte berechnen
						var driver = orderedQualifyingResult.FirstOrDefault(d => d.Driver.DriverId == bet.Place2.DriverId);
						punkte += CalculateBonusPoints(driver == null ? 8 : driver.Position);
					}
				}

				if (bet.Place3.DriverId == platz3Id)
				{
					//Richtig geraten = 5 Punkte
					punkte += 5;

					//Bonuspunkte berechnen
					var driver = orderedQualifyingResult.FirstOrDefault(d => d.Driver.DriverId == bet.Place3.DriverId);
					punkte += CalculateBonusPoints(driver == null ? 8 : driver.Position);
				}
				else
				{
					//Es kann sein dass der Tipp auf dem zweiten oder dritten Platz gelandet ist
					if (bet.Place3.DriverId == platz2Id || bet.Place3.DriverId == platz1Id)
					{
						//Der getippte Fahrer ist immerhin unter den Top 3 = 1 Punkt
						punkte += 1;

						//Bonuspunkte berechnen
						var driver = orderedQualifyingResult.FirstOrDefault(d => d.Driver.DriverId == bet.Place3.DriverId);
						punkte += CalculateBonusPoints(driver == null ? 8 : driver.Position);
					}
				}
			}
			catch (Exception ex)
			{
				//Wahrscheinlich gibt es zu diesem Tipp noch keine Ergebnisse
				//-> Es werden also 0 Punkte zurückgegeben
				return 0;
			}

			return punkte;
		}

		/// <summary>
		/// Berechnet die Bpnuspunkte für einen risikoreichen Tipp
		/// </summary>
		/// <param name="startPosition">Qualifying Position des Fahrers</param>
		/// <returns>Anzahl der Bonuspunkte abhängig von dem Risiko des Tipps</returns>
		private static int CalculateBonusPoints(int startPosition)
		{
			int bonus = 0;

			int diff = startPosition - 3;
			if (diff >= 1 && diff <= 2)
			{
				bonus = 1;
			}
			else if (diff >= 3 && diff <= 4)
			{
				bonus = 2;
			}
			else if (diff > 4)
			{
				bonus = 3;
			}

			return bonus;
		}

		/// <summary>
		/// Führt die Berechnung der Punkte an den übergebenen Tipps durch
		/// </summary>
		/// <param name="bets">Tipps die ausgewertet werden sollen</param>
		/// <returns>Die berechneten Punkte für die Tipps</returns>
		private static int CalculatePoints(IEnumerable<RaceBet> bets)
		{
			return bets.Sum(b => GetPointsForBet(b));
		}

		/// <summary>
		/// Berechnet die Punkte für einen Spieler für alle gespeicherten Wetten
		/// </summary>
		/// <param name="player">Der Spieler für den die Punkte berechnet werden sollen</param>
		/// <returns>Alle vom Spieler gesammelten Punkte</returns>
		public static int GetPointsForPlayer(UserAccount player)
		{
			if (player.RaceBets == null) { return 0; }

			IEnumerable<RaceBet> bets = player.RaceBets.Where(b => b.Place1 != null);

			int allPoints = CalculatePoints(bets);

			return allPoints;
		}

		/// <summary>
		/// Gibt an wieviele Plätze ein Spieler im Vergleich zum vorletzten
		/// Rennen gewonnen oder verloren hat.
		/// </summary>
		/// <param name="players">Spieler mit denen der Spieler verglichen werden soll</param>
		/// <param name="player">Der Spieler für den der Vergleich durchgeführt werden soll</param>
		/// <param name="lastRace">Das Rennen, das als Ausgangspunkt der Vergleichs dient</param>
		/// <returns>Anzahl der Plätze die der Spieler gewonnen oder verloren hat. Siehe Vorzeichen</returns>
		public static int GetChangeForPlayer(List<UserAccount> players, UserAccount player, Race lastRace)
		{
			Dictionary<UserAccount, int> alterStand = new Dictionary<UserAccount, int>();
			Dictionary<UserAccount, int> neuerStand = new Dictionary<UserAccount, int>();

			int pointsBefore = 0;
			int newPoints = 0;

			foreach (UserAccount p in players)
			{
				foreach (RaceBet b in p.RaceBets)
				{
					try
					{
						if (b.Race.RaceId != lastRace.RaceId)
						{
							pointsBefore += GetPointsForBet(b);
						}
						else
						{
							newPoints += GetPointsForBet(b);
						}
					}
					catch (NullReferenceException ex)
					{
						//Bei dem ersten Rennen der Saison gibt es noch kein lastRace
					}
				}

				alterStand.Add(p, pointsBefore);
				neuerStand.Add(p, pointsBefore + newPoints);

				newPoints = 0;
				pointsBefore = 0;
			}

			//Nach Punkten sortieren
			alterStand = alterStand.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
			neuerStand = neuerStand.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);


			int aPos = 1, nPos = 1;

			foreach (UserAccount p in alterStand.Keys)
			{
				if (p.Id == player.Id)
				{
					break;
				}
				aPos++;
			}

			foreach (UserAccount p in neuerStand.Keys)
			{
				if (p.Id == player.Id)
				{
					break;
				}
				nPos++;
			}

			int dif = nPos - aPos;

			return dif;
		}

		/// <summary>
		/// Berechnet die Position eines Spielers über alle Rennen
		/// </summary>
		/// <param name="players">Die Spieler mit denen der spieler verglichen werden soll</param>
		/// <param name="player">Der Spieler für den die Auswertung durchgeführt werden soll</param>
		/// <param name="races">Die Rennen die ausgewertet werden sollen</param>
		/// <returns>Positionen des Spielers und seine Punkte Pro Rennen</returns>
		public static SmallChartData GetPlayerPositionChanges(List<UserAccount> players, UserAccount player, List<Race> races)
		{
			LinkedList<int> playerPositions = new LinkedList<int>();
			LinkedList<int> playerPoints = new LinkedList<int>();   //TODO: mitverwenden

			int[] cummulatedPoints = new int[players.Count];

			foreach (Race r in races)
			{
				Dictionary<UserAccount, int> playerStandings = new Dictionary<UserAccount, int>();

				int playerId = 0;
				foreach (UserAccount p in players)
				{
					//Punkte aller Fahrer für das Rennen bestimmen
					int pointsInRace = p.RaceBets.Where(b => b.Race.RaceId == r.RaceId).Sum(b => GetPointsForBet(b));

					/* Der Obere Code ist testweise eingefügt um zu sehen ob der LINQ Query 
					 * Wie erwartet funktioniert. Wenn nicht wieder die untere Schleife verwenden
					 * um alle Punkte des Spielers zu erhalten
					 */

					//foreach (RaceBet b in p.RaceBets)
					//{
					//    if (b.Race.RaceId == r.RaceId)
					//    {
					//        pointsInRace += GetPointsForBet(b);
					//    }
					//}

					if (p.Id == player.Id)
					{
						playerPoints.AddLast(pointsInRace);
					}

					cummulatedPoints[playerId] += pointsInRace;

					playerStandings.Add(p, cummulatedPoints[playerId]);

					playerId++;
				}

				//Nach Punkten sortieren
				playerStandings = playerStandings.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

				//Position des Fahrers bestimmen
				for (int i = 0; i < playerStandings.Keys.Count; i++)
				{
					if (playerStandings.Keys.ElementAt(i).Id == player.Id)
					{
						playerPositions.AddLast(i + 1);
						break;
					}
				}
			}


			return new SmallChartData
			{
				PlayerPoints = playerPoints,
				PlayerPositions = playerPositions
			};
		}
	}

	public class SmallChartData
	{
		public LinkedList<int> PlayerPositions { get; set; }
		public LinkedList<int> PlayerPoints { get; set; }
	}
}