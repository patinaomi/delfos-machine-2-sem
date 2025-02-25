SET SERVEROUTPUT ON;

-- Nome dos Integrantes: 
-- Claudio Silva Bispo RM553472
-- Patricia Naomi Yamagishi RM552981

-- Drop das tabelas
DROP TABLE Tb_Usuario CASCADE CONSTRAINTS;
DROP TABLE Tb_Quarto CASCADE CONSTRAINTS;
DROP TABLE Tb_Reserva CASCADE CONSTRAINTS;
DROP TABLE Tb_Hospede CASCADE CONSTRAINTS;
DROP TABLE Tb_Pagamento CASCADE CONSTRAINTS;
DROP TABLE Tb_Fotos_Quarto CASCADE CONSTRAINTS;

-- Criar tabela de Usuários (Locador e Hóspede que realiza reservas)
CREATE TABLE Tb_Usuario (
    id_usuario INTEGER GENERATED ALWAYS AS IDENTITY(START WITH 1 INCREMENT BY 1) PRIMARY KEY,
    nome VARCHAR2(100) NOT NULL,
    sobrenome VARCHAR2(100) NOT NULL,
    cpf VARCHAR2(14) CONSTRAINT cpf_unique UNIQUE NOT NULL, 
    email VARCHAR2(100) CONSTRAINT email_unique UNIQUE NOT NULL, 
    telefone VARCHAR2(15),
    senha VARCHAR2(255) NOT NULL,
    tipo_usuario VARCHAR2(20) CHECK (tipo_usuario IN ('LOCADOR', 'HOSPEDE')) NOT NULL,
    data_cadastro DATE DEFAULT SYSDATE,
    updated_at DATE DEFAULT SYSDATE
);

-- Criar tabela de Quartos (Cadastrados pelos LOCADORES)
CREATE TABLE Tb_Quarto (
    id_quarto INTEGER GENERATED ALWAYS AS IDENTITY(START WITH 1 INCREMENT BY 1) PRIMARY KEY,
    id_usuario NUMBER NOT NULL, -- Quem cadastrou o quarto (LOCADOR)
    nome_quarto VARCHAR2(50) NOT NULL,
    descricao VARCHAR2(255),
    capacidade NUMBER NOT NULL,
    preco_diaria NUMBER(8,2) NOT NULL,
    disponivel CHAR(1) DEFAULT 'S' CHECK (disponivel IN ('S', 'N')),
    created_at DATE DEFAULT SYSDATE,
    updated_at DATE DEFAULT SYSDATE,
    CONSTRAINT fk_quarto_usuario FOREIGN KEY (id_usuario) REFERENCES Tb_Usuario(id_usuario) ON DELETE CASCADE
);

-- Criar tabela de Reservas (Feita por um usuário para um quarto)
CREATE TABLE Tb_Reserva (
    id_reserva INTEGER GENERATED ALWAYS AS IDENTITY(START WITH 1 INCREMENT BY 1) PRIMARY KEY,
    id_usuario NUMBER NOT NULL, -- Quem fez a reserva (HOSPEDE)
    id_quarto NUMBER NOT NULL, -- Qual quarto foi reservado
    data_checkin DATE NOT NULL,
    data_checkout DATE NOT NULL,
    status_reserva VARCHAR2(20) CHECK (status_reserva IN ('Confirmada', 'Cancelada', 'Em andamento')) NOT NULL,
    valor_total NUMBER(10,2),
    created_at DATE DEFAULT SYSDATE,
    updated_at DATE DEFAULT SYSDATE,
    CONSTRAINT fk_reserva_usuario FOREIGN KEY (id_usuario) REFERENCES Tb_Usuario(id_usuario) ON DELETE CASCADE,
    CONSTRAINT fk_reserva_quarto FOREIGN KEY (id_quarto) REFERENCES Tb_Quarto(id_quarto) ON DELETE CASCADE
);

-- Criar tabela de Hóspedes (Pessoas que estarão na reserva)
CREATE TABLE Tb_Hospede (
    id_hospede INTEGER GENERATED ALWAYS AS IDENTITY(START WITH 1 INCREMENT BY 1) PRIMARY KEY,
    id_reserva NUMBER NOT NULL, -- A qual reserva esse hóspede pertence
    nome VARCHAR2(100) NOT NULL,
    sobrenome VARCHAR2(100) NOT NULL,
    documento VARCHAR2(20) UNIQUE NOT NULL, -- CPF, RG, Passaporte
    telefone VARCHAR2(15),
    data_nascimento DATE,
    created_at DATE DEFAULT SYSDATE,
    CONSTRAINT fk_hospede_reserva FOREIGN KEY (id_reserva) REFERENCES Tb_Reserva(id_reserva) ON DELETE CASCADE
);

-- Criar tabela de Pagamentos (Ligado diretamente à reserva)
CREATE TABLE Tb_Pagamento (
    id_pagamento INTEGER GENERATED ALWAYS AS IDENTITY(START WITH 1 INCREMENT BY 1) PRIMARY KEY,
    id_reserva NUMBER NOT NULL,
    forma_pagamento VARCHAR2(20) CHECK (forma_pagamento IN ('Cartão', 'PIX', 'Dinheiro')),
    metodo_transacao VARCHAR2(20) CHECK (metodo_transacao IN ('À Vista', 'Parcelado')) NOT NULL,
    valor_pago NUMBER(10,2) NOT NULL,
    desconto NUMBER(10,2) DEFAULT 0,
    status_pagamento VARCHAR2(20) CHECK (status_pagamento IN ('Pago', 'Pendente', 'Cancelado')) NOT NULL,
    data_pagamento DATE DEFAULT SYSDATE,
    created_at DATE DEFAULT SYSDATE,
    updated_at DATE DEFAULT SYSDATE,
    CONSTRAINT fk_pagamento_reserva FOREIGN KEY (id_reserva) REFERENCES Tb_Reserva(id_reserva) ON DELETE CASCADE
);

-- Criar tabela de Fotos do Quarto
CREATE TABLE Tb_Fotos_Quarto (
    id_foto INTEGER GENERATED ALWAYS AS IDENTITY(START WITH 1 INCREMENT BY 1) PRIMARY KEY,
    id_quarto NUMBER NOT NULL,
    url_foto VARCHAR2(255) NOT NULL,
    created_at DATE DEFAULT SYSDATE,
    CONSTRAINT fk_fotos_quarto FOREIGN KEY (id_quarto) REFERENCES Tb_Quarto(id_quarto) ON DELETE CASCADE
);
