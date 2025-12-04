using UnityEngine;

public class TreeManager : MonoBehaviour
{
    class Tree
    {
        // I HATE getters and setters! Worthless overhead!
        public int levels, nodes;
        public int[] costs;
        public int[] structure; //each index corresponds to a node, the value corresponds to that nodes parent
        public string[] descriptions;

        public Tree(int levels, int nodes)
        {
            this.levels = levels;
            this.nodes = nodes;
        }
    }

    Tree[] skillTrees = new Tree[4];

    void InitializeTrees()
    {
        //Body Tree
        Tree body = new Tree(3, 6);
        body.structure  = new int[] { -1,  0,  0, -1,  3,  3 };
        body.costs      = new int[] {  1,  1,  1,  1,  1,  1 };
        body.descriptions = new string[] { "HP +1", "HP +1" , "HP +1" , "SPD +1" , "SPD +1" , "SPD +1" };
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
