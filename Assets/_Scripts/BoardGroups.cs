using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Group
{
    public int GroupId { get; set; }
    public CardColor Color { get; set; }
    public List<int> Slots { get; set; } = new();
}
public class BoardGroups : MonoBehaviour
{
    public Board Board;
    public List<Group> Groups = new();
    void Start()
    {
        Board = gameObject.GetComponentInParent<Board>();
    }

    public void AddToGroup(BoardSlot slot, List<int> adjSlots)
    {
        List<Group> matchingGroups = new List<Group>();
        //Get all matching groups
        foreach(var group in Groups)
        {
            for(int i = 0; i < adjSlots.Count; i++)
            {
                if (group.Slots.Contains(adjSlots[i]))
                {
                    matchingGroups.Add(group);
                }
            }
        }

        matchingGroups[0].Slots.Add(slot.SlotId);

        if (matchingGroups.Count > 0)
        {
            for (int i = 1; i < matchingGroups.Count; i++)
            {
                matchingGroups[0].Slots.AddRange(matchingGroups[i].Slots);
                Groups.Remove(matchingGroups[i]);
            }
            Groups.Remove(matchingGroups[0]);
            Groups.Add(matchingGroups[0]);
        }
    }

    public void CreateGroup(BoardSlot slot)
    {
        Group newGroup = new Group();
        newGroup.GroupId = Groups.Count == 0 ? 0 : Groups.Max(x => x.GroupId) + 1;
        newGroup.Color = slot.SlotColor;
        newGroup.Slots.Add(slot.SlotId);

        Groups.Add(newGroup);
    }

    public void HarvestGroup(BoardSlot slot)
    {
        int count = 0;
        Group group = Groups.Where(x => x.Slots.Contains(slot.SlotId)).FirstOrDefault();
        CardColor slotColor = slot.SlotColor;

        foreach (var id in group.Slots)
        {
            GameObject fill = Board.Slots[id].transform.GetChild(0).gameObject;
            Board.Slots[id].SlotColor = CardColor.None;
            Destroy(fill);
            count++;
        }
        Groups.Remove(group);

        Debug.Log("Harvesting group: " + count);
        //Call scoring function with count
        GameManager.Instance.ScoreManager.AddHarvestScore(count, slotColor);
    }
}

