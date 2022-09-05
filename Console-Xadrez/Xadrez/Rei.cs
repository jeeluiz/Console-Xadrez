using Tabuleiro;

namespace Xadrez
{
    public class Rei : Peca
    {
        public Rei(Cor cor, TabuleiroTab tab) : base(cor, tab)
        {

        }

        public override string ToString()
        {
            return "R";
        }
    }
}
