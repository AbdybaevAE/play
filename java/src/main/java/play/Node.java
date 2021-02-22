package play;

public class Node {
    int val;
    public Node left;
    public Node right;
    int height;
    Node(int val) {
        this.height = 1;
        this.val = val;
    }
}
