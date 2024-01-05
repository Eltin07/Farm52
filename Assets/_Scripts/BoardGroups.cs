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

    public void HarvestGroup(BoardSlot slot)
    {
        int count = 0;
        Group group = Groups.Where(x => x.Slots.Contains(slot.SlotId)).FirstOrDefault();
        CardColor slotColor = slot.SlotColor;
        GameManager.Instance.ScoreManager.AddHarvestScore(group);

        foreach (var id in group.Slots)
        {
            GameObject fill = Board.Slots[id].transform.GetChild(0).gameObject;
            Board.Slots[id].SlotColor = CardColor.None;
            Destroy(fill);
            count++;
        }
        Groups.Remove(group);
    }


    public void ShapeToGroup(List<GameObject> slots)
    {
        List<int> ShapeSlots = new();
        List<int> SlotsByShape = new();
        List<int> SameColorSlots = new();

        foreach(var slot in slots)
        {
            BoardSlot _slot = slot.GetComponent<BoardSlot>();
            SlotsByShape.AddRange(_slot.adjacentSlots);
            ShapeSlots.Add(_slot.SlotId);
        }

        SlotsByShape = SlotsByShape.Where(x => !ShapeSlots.Contains(x)).ToList();
  
        CardColor groupColor = Board.GetSlotById(ShapeSlots[0]).SlotColor;
        foreach (var id in SlotsByShape)
        {
            if (Board.GetSlotById(id).SlotColor == groupColor)
            {
                SameColorSlots.Add(id);
            }
        }
        SameColorSlots = SameColorSlots.Distinct().ToList();


        if (SameColorSlots.Count == 0)
            CreateGroup(ShapeSlots, groupColor);
        else
        {
            CreateGroup(ShapeSlots, groupColor);
            SameColorSlots.Add(ShapeSlots[0]);
            MergeGroups(SameColorSlots);
        }
    }

    public void CreateGroup(List<int> slotIds, CardColor color)
    {
        Group newGroup = new Group();
        newGroup.GroupId = Groups.Count == 0 ? 0 : Groups.Max(x => x.GroupId) + 1;
        newGroup.Color = color;
        newGroup.Slots.AddRange(slotIds);

        Groups.Add(newGroup);
    }

    public void MergeGroups(List<int> slotIds)
    {
        List<Group> GroupsToMerge = new();

        foreach (var id in slotIds)
        {
            Group group = Groups.Where(x => x.Slots.Contains(id)).FirstOrDefault();
            GroupsToMerge.Add(group);
        }

        GroupsToMerge = GroupsToMerge.Distinct().ToList();
        Debug.Log("Groups to merge " + GroupsToMerge.Count);

        Group newGroup = new Group();
        newGroup.GroupId = Groups.Count == 0 ? 0 : Groups.Max(x => x.GroupId) + 1;
        newGroup.Color = GroupsToMerge[0].Color;

        foreach (var group in GroupsToMerge)
        {
            newGroup.Slots.AddRange(group.Slots);
            Groups.Remove(group);
        }

        Groups.Add(newGroup);
    }
}

