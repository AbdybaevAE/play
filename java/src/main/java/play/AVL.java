package play;

public class AVL {
    private Node root;
    private int height(Node node) {
        if (node == null) return 0;
        return node.height;
    }
    int balance(Node node) {
        if (node == null) return 0;
        return height(node.left) - height(node.right);
    } 
    private Node leftRotate(Node node) {
        Node retNode = node.right;
        Node leftNode = retNode.left;
        retNode.left = node;
        node.right = leftNode;
        return retNode;
    }
    private Node rightRotate(Node node) {
        Node retNode = node.left;
        Node rightNode = retNode.right;
        retNode.right = node;
        node.left = rightNode;
        return retNode;
    }
    public void insert(int val) {
        root = insertRecursively(root, val);
    }
    private Node insertRecursively(Node node, int val) {
        if (node == null) return new Node(val);
        if (node.val < val) {
            node.right = insertRecursively(node.right, val);
        } else if (node.val > val) {
            node.left = insertRecursively(node.left, val);
        } else {
            return node;
        }
        node.height = 1 + Math.max(height(node.left), height(node.right));
        int bal = balance(node);
        if (bal > 1 && val < node.left.val) {
            return rightRotate(node);
        }
        if (bal < -1 && val > node.right.val) {
            return leftRotate(node);
        }
        if (bal > 1 && val > node.left.val) {
            node.left = leftRotate(node.left);
            return rightRotate(node);
        }
        if (bal < - 1 && val < node.right.val) {
            node.right = rightRotate(node.right);
            return leftRotate(node);
        }
        return node;
    }
    public void inorder() {
        inorderRecursively(root);
    }
    private void inorderRecursively(Node node) {
        if (node == null) return;
        inorderRecursively(node.left);
        System.out.println(node.val);
        inorderRecursively(node.right);
    }
}
