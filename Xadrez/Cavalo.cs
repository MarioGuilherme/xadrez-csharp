using TabuleiroNM;

namespace Xadrez {
    class Cavalo : Peca {
        public Cavalo(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor) { }

        public override string ToString() {
            return "C";
        }

        private bool PodeMover(Posicao posicao) {
            Peca peca = Tabuleiro.Peca(posicao);
            return peca == null || peca.Cor != Cor;
        }

        public override bool[,] MovimentosPossiveis() {
            bool[,] movimentosPossiveis = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];
            Posicao posicao = new(0, 0);

            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 2);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

            posicao.DefinirValores(Posicao.Linha - 2, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

            posicao.DefinirValores(Posicao.Linha - 2, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 2);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 2);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

            posicao.DefinirValores(Posicao.Linha + 2, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

            posicao.DefinirValores(Posicao.Linha + 2, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 2);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

            return movimentosPossiveis;
        }
    }
}