using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerCombo
{
    List<PlayerCombo> _playerCombos;

    List<PossibleCombo> possibleCombos = new List<PossibleCombo>();

    public CheckPlayerCombo(List<PlayerCombo> playerCombos)
    {
        _playerCombos = playerCombos;
    }

    public int Check(Cardinal cardinal)
    {
        List<PossibleCombo> remove = new List<PossibleCombo>();
        for(int i = 0; i < possibleCombos.Count; i++)
        {
            PossibleCombo possibleCombo = possibleCombos[i];
            if (possibleCombo.playerCombo.cardinals[possibleCombo.step + 1] == cardinal)
            {
                if(possibleCombo.playerCombo.cardinals.Count == possibleCombo.step + 2)
                {
                    possibleCombos.Clear();
                    return possibleCombo.playerCombo.value;
                } else {
                    possibleCombo.step++;
                }
            } else {
                remove.Add(possibleCombo);
            }
        }

        foreach (PossibleCombo possibleCombo in remove)
        {
            possibleCombos.Remove(possibleCombo);
        }

        foreach (PlayerCombo playerCombo in _playerCombos)
        {
            if (playerCombo.cardinals[0] == cardinal)
            {
                possibleCombos.Add(new PossibleCombo(playerCombo, 0));
            }
        }

        return 0;
    }

    class PossibleCombo
    {
        public PlayerCombo playerCombo;
        public int step;

        public PossibleCombo(PlayerCombo playerCombo, int step)
        {
            this.playerCombo = playerCombo; //para todos los que lean. Quiero que sepan que odio a MirchMax
            this.step = step;
        }
    }
}
