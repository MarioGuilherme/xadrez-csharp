using TabuleiroNM;

namespace Xadrez {
    class Rei : Peca {
        private PartidaDeXadrez PartidaDeXadrez;

        public Rei(Tabuleiro tabuleiro, Cor cor, PartidaDeXadrez partidaDeXadrez) : base(tabuleiro, cor) {
            PartidaDeXadrez = partidaDeXadrez;
        }

        public override string ToString() {
            return "R";
        }

        private bool PodeMover(Posicao posicao) {
            Peca peca = Tabuleiro.Peca(posicao);
            return peca == null || peca.Cor != Cor;
        }

        private bool TesteTorreParaRoque(Posicao posicao) {
            Peca peca = Tabuleiro.Peca(posicao);
            return peca != null && peca is Torre && peca.Cor == Cor && peca.QuantidadeMovimentos == 0;
        }

        public override bool[,] MovimentosPossiveis() {
            bool[,] movimentosPossiveis = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];
            Posicao posicao = new(0, 0);

            // Acima
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

            // Nordeste
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

            // Direita
            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

            // Sudeste
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

            // Abaixo
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

            // Sudoeste
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

            // Esquerda
            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

            // Noroeste
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

            // #Jogada Especial - Roque
            if (QuantidadeMovimentos == 0 && !PartidaDeXadrez.Xeque) {
                // #Roque pequeno
                Posicao posicaoTorre1 = new(Posicao.Linha, Posicao.Coluna + 3);
                if (TesteTorreParaRoque(posicaoTorre1)) {
                    Posicao posicao1 = new(Posicao.Linha, Posicao.Coluna + 1);
                    Posicao posicao2 = new(Posicao.Linha, Posicao.Coluna + 2);
                    if (Tabuleiro.Peca(posicao1) == null && Tabuleiro.Peca(posicao2) == null)
                        movimentosPossiveis[Posicao.Linha, Posicao.Coluna + 2] = true;
                }
                // #Roque grande
                Posicao posicaoTorre2 = new(Posicao.Linha, Posicao.Coluna - 4);
                if (TesteTorreParaRoque(posicaoTorre2)) {
                    Posicao posicao1 = new(Posicao.Linha, Posicao.Coluna - 1);
                    Posicao posicao2 = new(Posicao.Linha, Posicao.Coluna - 2);
                    Posicao posicao3 = new(Posicao.Linha, Posicao.Coluna - 3);
                    if (Tabuleiro.Peca(posicao1) == null && Tabuleiro.Peca(posicao2) == null && Tabuleiro.Peca(posicao3) == null)
                        movimentosPossiveis[Posicao.Linha, Posicao.Coluna - 2] = true;
                }
            }

            return movimentosPossiveis;
        }
    }
}