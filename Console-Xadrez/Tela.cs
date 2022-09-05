using Tabuleiro;

namespace Console_Xadrez
{
    public class Tela
    {
        public static void imprimirTabuleiro(TabuleiroTab tab)
        {
            for (int i = 0; i < tab.linhas; i++)
            {
                for(int j=0; j < tab.colunas; j++)
                {
                    Console.Write("- ");
                    Console.Write(tab.peca(i,j) + " ");
                }
                Console.WriteLine();
            }
        }
        
    }
}
