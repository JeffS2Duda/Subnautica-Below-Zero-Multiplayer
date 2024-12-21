namespace Subnautica.Network.Models.Storage.World.Childrens
{
    using MessagePack;
    using Subnautica.Network.Structures;
    using System.Collections.Generic;
    using System.Linq;

    [MessagePackObject]
    public class Base
    {
        [Key(0)]
        public string BaseId { get; set; }

        [Key(1)]
        public ZeroColor BaseColor { get; set; }

        [Key(2)]
        public ZeroColor StripeColor1 { get; set; }

        [Key(3)]
        public ZeroColor StripeColor2 { get; set; }

        [Key(4)]
        public ZeroColor NameColor { get; set; }

        [Key(5)]
        public string Name { get; set; }

        [Key(6)]
        public HashSet<ZeroInt3> DisablePowers { get; set; } = new HashSet<ZeroInt3>();

        [Key(7)]
        public Dictionary<string, ZeroVector3> MinimapPositions { get; set; } = new Dictionary<string, ZeroVector3>();

        [Key(8)]
        public HashSet<Leaker> Leakers { get; set; } = new HashSet<Leaker>();

        [Key(9)]
        public Dictionary<ushort, float> CellWaterLevels { get; set; } = new Dictionary<ushort, float>();

        public void SetColorCustomizer(string name, ZeroColor baseColor, ZeroColor stripeColor1, ZeroColor stripeColor2, ZeroColor nameColor)
        {
            this.Name = name;
            this.BaseColor = baseColor;
            this.StripeColor1 = stripeColor1;
            this.StripeColor2 = stripeColor2;
            this.NameColor = nameColor;
        }

        public void SetCellWaterLevel(ushort index, float waterLevel)
        {
            if (waterLevel > 0f)
            {
                this.CellWaterLevels[index] = waterLevel;
            }
            else
            {
                this.CellWaterLevels.Remove(index);
            }
        }

        public void RemoveConstruction(string constructionId, ZeroInt3 cell)
        {
            if (cell != null)
            {
                this.DisablePowers.RemoveWhere(q => q == cell);
            }

            this.MinimapPositions.Remove(constructionId);
            this.Leakers.RemoveWhere(q => q.UniqueId == constructionId);
        }

        public bool TryGetLeaker(string uniqueId, out Leaker leaker)
        {
            leaker = this.Leakers.FirstOrDefault(q => q.UniqueId == uniqueId);
            if (leaker == null)
            {
                leaker = new Leaker()
                {
                    UniqueId = uniqueId,
                };

                this.Leakers.Add(leaker);
            }

            return true;
        }

        public bool UpdateLeakPoints(string uniqueId, float currentHealth, float maxHealth, List<ZeroVector3> leakPoints = null, ZeroVector3 playerPosition = null)
        {
            if (this.TryGetLeaker(uniqueId, out var leaker))
            {
                if (leakPoints != null)
                {
                    leaker.UpdateMaxLeakCount(leakPoints.Count);
                }

                var numLeakPoints = global::Leakable.ComputeNumLeakPoints(currentHealth / maxHealth, leaker.MaxLeakCount);
                if (leaker.GetLeakCount() == numLeakPoints)
                {
                    return false;
                }

                numLeakPoints -= leaker.GetLeakCount();

                if (playerPosition == null)
                {
                    foreach (var point in leakPoints)
                    {
                        if (numLeakPoints > 0 && !leaker.Points.Any(q => q == point))
                        {
                            numLeakPoints--;

                            leaker.Points.Add(point);
                        }
                    }
                }
                else
                {
                    while (numLeakPoints < 0)
                    {
                        numLeakPoints++;

                        var lastDistance = 999999f;
                        var lastIndex = -1;

                        for (int i = 0; i < leaker.Points.Count; i++)
                        {
                            var distance = playerPosition.Distance(leaker.Points.ElementAt(i));
                            if (distance < lastDistance)
                            {
                                lastDistance = distance;
                                lastIndex = i;
                            }
                        }

                        if (lastIndex != -1)
                        {
                            leaker.Points.RemoveAt(lastIndex);
                        }
                    }
                }
            }

            return true;
        }
    }

    [MessagePackObject]
    public class Leaker
    {
        [Key(0)]
        public string UniqueId { get; set; }

        [Key(1)]
        public List<ZeroVector3> Points { get; set; } = new List<ZeroVector3>();

        [Key(2)]
        public byte MaxLeakCount { get; set; }

        public void UpdateMaxLeakCount(int maxLeakCount)
        {
            if (this.MaxLeakCount == 0)
            {
                this.MaxLeakCount = (byte)maxLeakCount;
            }
        }

        public int GetLeakCount()
        {
            return this.Points.Count;
        }
    }
}
