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

    //Getters & Setters
    public int getCodigo() {
        return codigo;
    }

    public void setCodigo(int codigo) {
        this.codigo = codigo;
    }

    public String getDescricao() {
        return descricao;
    }

    public void setDescricao(String descricao) {
        this.descricao = descricao;
    }

    public float getPrecoUn() {
        return precoUn;
    }

    public void setPrecoUn(float precoUn) {
        this.precoUn = precoUn;
    }


    //Impressão de Dados
    @Override
    public String toString() {
        return "Cod #" + this.getCodigo() + "  -  " + this.getDescricao() + "  R$" + this.getPrecoUn();
    }
}