namespace Tabuleiro
{
    public abstract class Peca
    {
        public Posicao posicao { get; set; }
        public Cor cor { get; protected set; }
        public int qteMovimentos { get;protected set; }
        public TabuleiroTab tab { get; protected set; }

        public Peca(Cor cor, TabuleiroTab tab)
        {
            this.posicao = null;
            this.cor = cor;
            this.qteMovimentos = 0;
            this.tab = tab;
        }

        public void IncrementarQteMovimentos()
        {
            qteMovimentos++;
        }

        public abstract bool[,] MovimentosPossiveis();
       
    }
}

