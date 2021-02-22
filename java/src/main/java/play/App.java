package play;

import java.util.List;

public class App 
{
   
    public static void main( String[] args )
    {   
        List<Integer> list = new ArrayList<>();
        list.add(1);
        int [] ar = new int [1];
        ar = list.toArray();
    }
}
