using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TAPI;

namespace Avalon.NPCs.Normal.Worms
{
    /// <summary>
    /// The Dark Matter Spearworm's head.
    /// </summary>
    public sealed class DMSpearwormHead : ModNPC
    {
        bool TailSpawned = false;
        /// <summary>
        /// 
        /// </summary>
        public override void AI()
        {
            if (!TailSpawned)
            {
                int Previous = npc.whoAmI;
                for (int Counter = 0; Counter < 14; Counter++)
                {
                    int spawn = 0;
                    if (Counter < 10) //Leaves four segments for a tail, for the unique look.
                    {
                        spawn = NPC.NewNPC((int)npc.position.X + (npc.width / 2), (int)npc.position.Y + (npc.height / 2), NPCDef.byName["Avalon:Dark Matter Spearworm Body"].type, npc.whoAmI);
                    }
                    else
                    {
                        spawn = NPC.NewNPC((int)npc.position.X + (npc.width / 2), (int)npc.position.Y + (npc.height / 2), NPCDef.byName["Avalon:Dark Matter Spearworm Tail"].type, npc.whoAmI);
                    }
                    Main.npc[spawn].realLife = npc.whoAmI;
                    Main.npc[spawn].ai[2] = (float)npc.whoAmI;
                    Main.npc[spawn].ai[1] = (float)Previous;
                    Main.npc[Previous].ai[0] = (float)spawn;
                    NetMessage.SendData(23, -1, -1, "", spawn, 0f, 0f, 0f, 0);
                    Previous = spawn;
                }

                TailSpawned = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="type"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public override bool CanSpawn(int x, int y, int type, Player p)
        {
            return AvalonMod.DarkMatter.Check(p) && Main.hardMode && Main.rand.Next(8) == 1;
        }
    }
}
