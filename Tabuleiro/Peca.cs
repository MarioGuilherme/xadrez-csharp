namespace TabuleiroNM {
    class Peca {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QuantidadeMovimentos { get; protected set; }
        public Tabuleiro Tabuleiro { get; protected set; }

        public Peca(Posicao posicao, Cor cor, Tabuleiro tabuleiro) {
            Posicao = posicao;
            Cor = cor;
            QuantidadeMovimentos = 0;
            Tabuleiro = tabuleiro;
        }
    }
}