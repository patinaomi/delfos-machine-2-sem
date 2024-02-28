package cp4;

public class Produto {
    //Atributos
    private int codigo;
    private String descricao;
    private float precoUn;

    //Construtores
    public Produto() {

    }

    public Produto(int codigo, String descricao, float precoUn) {
        this.codigo = codigo;
        this.descricao = descricao;
        this.precoUn = precoUn;
    }

    //Getters

    public int getCodigo() {
        return codigo;
    }

    public String getDescricao() {
        return descricao;
    }

    public float getPrecoUn() {
        return precoUn;
    }


    //toString para formatação dos dados
    @Override
    public String toString() {
        return "Cod #" + this.getCodigo() + "  -  " + this.getDescricao() + "  R$" + this.getPrecoUn();
    }
}