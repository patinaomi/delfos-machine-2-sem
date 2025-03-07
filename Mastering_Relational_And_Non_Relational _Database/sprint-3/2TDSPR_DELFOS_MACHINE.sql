SET SERVEROUTPUT ON;
-- Nome dos Integrantes: 
-- Claudio Silva Bispo RM553472
-- Patricia Naomi Yamagishi RM552981

-- Deletar tabelas caso já existam

DROP TABLE Notificacao CASCADE CONSTRAINTS;
DROP TABLE Agenda CASCADE CONSTRAINTS;
DROP TABLE Formulario_Detalhado CASCADE CONSTRAINTS;
DROP TABLE Feedback CASCADE CONSTRAINTS;
DROP TABLE Sinistro CASCADE CONSTRAINTS;
DROP TABLE Consulta CASCADE CONSTRAINTS;
DROP TABLE Dentista CASCADE CONSTRAINTS;
DROP TABLE Especialidade CASCADE CONSTRAINTS;
DROP TABLE Clinica CASCADE CONSTRAINTS;
DROP TABLE Cliente CASCADE CONSTRAINTS;
DROP TABLE Estado_Civil CASCADE CONSTRAINTS;
DROP TABLE Tipo_Notificacao CASCADE CONSTRAINTS;

-- Criação das Tabelas

-- Tabela Cliente 
CREATE TABLE Cliente ( 
    id_cliente INTEGER GENERATED ALWAYS AS IDENTITY(START WITH 1 INCREMENT BY 1) NOT NULL PRIMARY KEY,
    nome VARCHAR2(100) NOT NULL, 
    sobrenome VARCHAR2(100) NOT NULL,
    email VARCHAR2(100) CONSTRAINT email_unique UNIQUE NOT NULL, 
    telefone VARCHAR2(15) NOT NULL, 
    data_nasc DATE NOT NULL, 
    endereco VARCHAR2(255) NOT NULL
); 

-- Tabela Clínica 
CREATE TABLE Clinica ( 
    id_clinica INTEGER GENERATED ALWAYS AS IDENTITY(START WITH 1 INCREMENT BY 1) NOT NULL PRIMARY KEY,
    nome VARCHAR2(100) NOT NULL, 
    endereco VARCHAR2(255) NOT NULL, 
    telefone VARCHAR2(15) NOT NULL, 
    avaliacao DECIMAL(2, 1) NOT NULL,
    preco_medio DECIMAL(10, 2) NOT NULL
); 

-- Tabela Especialidade
CREATE TABLE Especialidade (
    id_especialidade INTEGER GENERATED ALWAYS AS IDENTITY(START WITH 1 INCREMENT BY 1) NOT NULL PRIMARY KEY,
    nome VARCHAR2(100) NOT NULL
);

-- Tabela Dentista
CREATE TABLE Dentista (
    id_dentista INTEGER GENERATED ALWAYS AS IDENTITY(START WITH 1 INCREMENT BY 1) NOT NULL PRIMARY KEY,
    nome VARCHAR2(100) NOT NULL,
    sobrenome VARCHAR2(100) NOT NULL,
    telefone VARCHAR2(15) NOT NULL,
    id_clinica INTEGER NOT NULL,
    id_especialidade INTEGER NOT NULL,
    avaliacao DECIMAL(2, 1) NOT NULL,
    
    CONSTRAINT fk_dentista_clinica FOREIGN KEY (id_clinica) REFERENCES Clinica(id_clinica),
    CONSTRAINT fk_dentista_especialidade FOREIGN KEY (id_especialidade) REFERENCES Especialidade(id_especialidade)
);

-- Tabela Consulta 
CREATE TABLE Consulta ( 
    id_consulta INTEGER GENERATED ALWAYS AS IDENTITY(START WITH 1 INCREMENT BY 1) NOT NULL PRIMARY KEY,
    id_cliente INTEGER NOT NULL, 
    id_clinica INTEGER NOT NULL, 
    id_dentista INTEGER NOT NULL,
    tipo_servico VARCHAR2(100) NOT NULL,
    data_consulta TIMESTAMP NOT NULL, 
    status_consulta CHAR(1), -- 'S' ou 'N'
    observacoes VARCHAR2(250), 
    sintomas VARCHAR2(250), 
    tratamento_recomendado VARCHAR2(250), 
    custo DECIMAL(10, 2),
    prescricao VARCHAR2(250), 
    data_retorno DATE,
    
    CONSTRAINT fk_consulta_cliente FOREIGN KEY (id_cliente) REFERENCES Cliente(id_cliente),
    CONSTRAINT fk_consulta_clinica FOREIGN KEY (id_clinica) REFERENCES Clinica(id_clinica),
    CONSTRAINT fk_consulta_dentista FOREIGN KEY (id_dentista) REFERENCES Dentista(id_dentista)
);
 
-- Tabela Sinistro
CREATE TABLE Sinistro (
    id_sinistro INTEGER GENERATED ALWAYS AS IDENTITY(START WITH 1 INCREMENT BY 1) NOT NULL PRIMARY KEY,
    id_consulta INTEGER NOT NULL, 
    nome VARCHAR2(100) NOT NULL,
    descricao VARCHAR2(250),
    status_sinistro CHAR(1), -- 'S' ou 'N'
    descricao_status VARCHAR2(250),
    valor_sinistro DECIMAL(10, 2),
    data_abertura DATE NOT NULL,
    data_resolucao DATE,
    documentacao VARCHAR2(250),
    
    CONSTRAINT fk_sinistro_consulta FOREIGN KEY (id_consulta) REFERENCES Consulta(id_consulta)
);

-- Tabela Feedback
CREATE TABLE Feedback (
    id_feedback INTEGER GENERATED ALWAYS AS IDENTITY(START WITH 1 INCREMENT BY 1) NOT NULL PRIMARY KEY,
    id_cliente INTEGER NOT NULL,
    id_dentista INTEGER NOT NULL,
    id_clinica INTEGER NOT NULL,
    avaliacao DECIMAL(2, 1) NOT NULL,
    comentario VARCHAR2(250),
    
    CONSTRAINT fk_feedback_cliente FOREIGN KEY (id_cliente) REFERENCES Cliente(id_cliente),
    CONSTRAINT fk_feedback_clinica FOREIGN KEY (id_clinica) REFERENCES Clinica(id_clinica),
    CONSTRAINT fk_feedback_dentista FOREIGN KEY (id_dentista) REFERENCES Dentista(id_dentista)
);
  
-- Tabela Estado Civil
CREATE TABLE Estado_Civil (
    id_estado_civil INTEGER GENERATED ALWAYS AS IDENTITY(START WITH 1 INCREMENT BY 1) NOT NULL PRIMARY KEY,
    descricao VARCHAR2(50) NOT NULL
);

-- Tabela Formulário Detalhado
CREATE TABLE Formulario_Detalhado (
    id_formulario INTEGER GENERATED ALWAYS AS IDENTITY(START WITH 1 INCREMENT BY 1) NOT NULL PRIMARY KEY,
    id_cliente INTEGER NOT NULL,
    id_estado_civil INTEGER NOT NULL,
    historico_familiar VARCHAR2(250),
    profissao VARCHAR2(100),
    renda_mensal DECIMAL(10, 2),
    historico_medico VARCHAR2(250),
    alergia VARCHAR2(250),
    condicao_preexistente VARCHAR2(250),
    uso_medicamento VARCHAR2(250),
    familiar_com_doencas_dentarias VARCHAR2(255),
    participacao_em_programas_preventivos CHAR(1), -- 'Y' ou 'N'
    contato_emergencial VARCHAR2(250),
    pesquisa_satisfacao CHAR(1), -- 'Y' ou 'N'
    data_ultima_atualizacao DATE,
    frequencia_consulta_periodica CHAR(1), -- 'Y' ou 'N'
    sinalizacao_de_risco VARCHAR2(250),
    historico_de_viagem VARCHAR2(250),
    historico_de_mudancas_de_endereco VARCHAR2(250),
    preferencia_de_contato VARCHAR2(50),

    CONSTRAINT fk_formulario_cliente FOREIGN KEY (id_cliente) REFERENCES Cliente(id_cliente),
    CONSTRAINT fk_formulario_estado_civil FOREIGN KEY (id_estado_civil) REFERENCES Estado_Civil(id_estado_civil)
);

-- Tabela Tipo Notificação
CREATE TABLE Tipo_Notificacao (
    id_tipo_notificacao INTEGER GENERATED ALWAYS AS IDENTITY(START WITH 1 INCREMENT BY 1) NOT NULL PRIMARY KEY,
    descricao VARCHAR2(40) NOT NULL
);

-- Tabela Agenda
CREATE TABLE Agenda (
    id_agenda INTEGER GENERATED ALWAYS AS IDENTITY(START WITH 1 INCREMENT BY 1) NOT NULL PRIMARY KEY,
    id_cliente INTEGER NOT NULL,
    id_consulta INTEGER NOT NULL,
    status_consulta CHAR(1),
    observacoes VARCHAR2(250),

    CONSTRAINT fk_agenda_cliente FOREIGN KEY (id_cliente) REFERENCES Cliente(id_cliente),
    CONSTRAINT fk_agenda_consulta FOREIGN KEY (id_consulta) REFERENCES Consulta(id_consulta)
);

-- Tabela Notificações
CREATE TABLE Notificacao (
    id_notificacao INTEGER GENERATED ALWAYS AS IDENTITY(START WITH 1 INCREMENT BY 1) NOT NULL PRIMARY KEY,
    id_cliente INTEGER NOT NULL,
    id_tipo_notificacao INTEGER NOT NULL,
    mensagem VARCHAR2(250),
    data_envio DATE,

    CONSTRAINT fk_notificacao_cliente FOREIGN KEY (id_cliente) REFERENCES Cliente(id_cliente),
    CONSTRAINT fk_notificacao_tipo FOREIGN KEY (id_tipo_notificacao) REFERENCES Tipo_Notificacao(id_tipo_notificacao)
);


/*

    Tarefas para esta entrega
    1. Correções - Não houve apontamentos
    2. Empacotamento de Objetos
    3. Criação de Procedures para Relatórios(Consultas)
    4. Criação de Trigger para Auditorias
    5. Testes e Demonstração em Vídeo
    
*/


-- 2. Empacotamento de Objetos

/*

    Vamos criar um package chamado clientes, clinicas,  que conterá as principais operações do sistema.
    
    Operações a incluir no Package:
        Uma procedure para inserir um cliente.
        Uma procedure para atualizar dados do cliente.
        Uma procedure para deletar um cliente.
        Uma função para buscar um cliente pelo ID.

*/

CREATE OR REPLACE PACKAGE BODY pkg_clientes AS 

    -- 1. Procedure para inserir um cliente
    PROCEDURE inserir_cliente(p_nome IN VARCHAR2, p_sobrenome IN VARCHAR2, p_email IN VARCHAR2, p_telefone IN VARCHAR2, p_data_nascimento IN DATE, p_endereco IN VARCHAR2) IS
    BEGIN
        INSERT INTO Cliente (nome, sobrenome, email, telefone, data_nasc, endereco) 
        VALUES (p_nome, p_sobrenome, p_email, p_telefone, p_data_nascimento, p_endereco);
        
        COMMIT;
    END inserir_cliente;

    -- 2. Procedure para atualizar um cliente
    PROCEDURE atualizar_cliente(p_id IN NUMBER, p_nome IN VARCHAR2, p_sobrenome IN VARCHAR2, p_email IN VARCHAR2, p_telefone IN VARCHAR2, p_data_nascimento IN DATE, p_endereco IN VARCHAR2) IS
    BEGIN
        UPDATE Cliente
        SET nome = p_nome, sobrenome = p_sobrenome, email = p_email, telefone = p_telefone, data_nasc = p_data_nascimento, endereco = p_endereco
        WHERE id_cliente = p_id;
        
        COMMIT;
    END atualizar_cliente;

    -- 3. Procedure para excluir um cliente
    PROCEDURE excluir_cliente(p_id IN NUMBER) IS
    BEGIN
        DELETE FROM Cliente WHERE id_cliente = p_id;
        
        COMMIT;
    END excluir_cliente;

    -- 4. Procedure para listar clientes (retorna um cursor para ser consumido pela aplicação)
    PROCEDURE listar_clientes(p_cursor OUT SYS_REFCURSOR) IS
    BEGIN
        OPEN p_cursor FOR 
            SELECT id_cliente, nome, sobrenome, email, telefone, data_nasc, endereco 
            FROM Cliente;
    END listar_clientes;

END pkg_clientes;

-- Executar a procedure inserir_cliente
BEGIN
    pkg_clientes.inserir_cliente(
        p_nome => 'João',
        p_sobrenome => 'Silva',
        p_email => 'joao.silva@email.com',
        p_telefone => '1234567890',
        p_data_nascimento => TO_DATE('1990-01-01', 'YYYY-MM-DD'),
        p_endereco => 'Rua A, 123'
    );
END;

SELECT * FROM CLIENTE

-- Executar a procedure atualizar_cliente

BEGIN
    pkg_clientes.atualizar_cliente(
        p_id => 1,
        p_nome => 'João',
        p_sobrenome => 'Silva',
        p_email => 'joao.silva@novoemail.com',
        p_telefone => '9876543210',
        p_data_nascimento => TO_DATE('1990-01-01', 'YYYY-MM-DD'),
        p_endereco => 'Rua B, 456'
    );
END;

SELECT * FROM CLIENTE

-- Executar a procedure excluir_cliente

BEGIN
    pkg_clientes.excluir_cliente(p_id => 1);
END;

SELECT * FROM CLIENTE

-- Executar a procedure listar_clientes
DECLARE
    v_cursor SYS_REFCURSOR; 
    v_id_cliente NUMBER;     
    v_nome VARCHAR2(100);
    v_sobrenome VARCHAR2(100);
    v_email VARCHAR2(100);
    v_telefone VARCHAR2(15);
    v_data_nasc DATE;
    v_endereco VARCHAR2(255);
BEGIN
    pkg_clientes.listar_clientes(p_cursor => v_cursor);

    LOOP
        FETCH v_cursor INTO v_id_cliente, v_nome, v_sobrenome, v_email, v_telefone, v_data_nasc, v_endereco;
        EXIT WHEN v_cursor%NOTFOUND;  -- Sai do loop quando o cursor não encontrar mais dados

        DBMS_OUTPUT.PUT_LINE('ID: ' || v_id_cliente || 
                             ', Nome: ' || v_nome || 
                             ', Sobrenome: ' || v_sobrenome || 
                             ', Email: ' || v_email || 
                             ', Telefone: ' || v_telefone || 
                             ', Data de Nascimento: ' || TO_CHAR(v_data_nasc, 'DD/MM/YYYY') || 
                             ', Endereço: ' || v_endereco);
    END LOOP;

    CLOSE v_cursor;
END;

-- Package para a tabela Clinica
CREATE OR REPLACE PACKAGE pkg_clinica AS 
    PROCEDURE inserir_clinica(p_nome IN VARCHAR2, p_endereco IN VARCHAR2, p_telefone IN VARCHAR2, p_avaliacao IN DECIMAL, p_preco_medio IN DECIMAL);
    PROCEDURE atualizar_clinica(p_id IN NUMBER, p_nome IN VARCHAR2, p_endereco IN VARCHAR2, p_telefone IN VARCHAR2, p_avaliacao IN DECIMAL, p_preco_medio IN DECIMAL);
    PROCEDURE excluir_clinica(p_id IN NUMBER);
    PROCEDURE listar_clinicas(p_cursor OUT SYS_REFCURSOR);
END pkg_clinica;

-- Package Body para a tabela Clinica
CREATE OR REPLACE PACKAGE BODY pkg_clinica AS 

    PROCEDURE inserir_clinica(p_nome IN VARCHAR2, p_endereco IN VARCHAR2, p_telefone IN VARCHAR2, p_avaliacao IN DECIMAL, p_preco_medio IN DECIMAL) IS
    BEGIN
        INSERT INTO CLINICA (nome, endereco, telefone, avaliacao, preco_medio) 
        VALUES (p_nome, p_endereco, p_telefone, p_avaliacao, p_preco_medio);
        
        COMMIT;
    END inserir_clinica;

    PROCEDURE atualizar_clinica(p_id IN NUMBER, p_nome IN VARCHAR2, p_endereco IN VARCHAR2, p_telefone IN VARCHAR2, p_avaliacao IN DECIMAL, p_preco_medio IN DECIMAL) IS
    BEGIN
        UPDATE CLINICA
        SET nome = p_nome, endereco = p_endereco, telefone = p_telefone, avaliacao = p_avaliacao, preco_medio = p_preco_medio
        WHERE id_clinica = p_id;
        
        COMMIT;
    END atualizar_clinica;

    PROCEDURE excluir_clinica(p_id IN NUMBER) IS
    BEGIN
        DELETE FROM CLINICA WHERE id_clinica = p_id;
        
        COMMIT;
    END excluir_clinica;

    PROCEDURE listar_clinicas(p_cursor OUT SYS_REFCURSOR) IS
    BEGIN
        OPEN p_cursor FOR 
            SELECT id_clinica, nome, endereco, telefone, avaliacao, preco_medio 
            FROM CLINICA;
    END listar_clinicas;

END pkg_clinica;

-- Teste de Inserção de Clínica (CRUD: Inserir)
BEGIN
    pkg_clinica.inserir_clinica(
        p_nome => 'Clínica São João',
        p_endereco => 'Rua das Flores, 123, Centro',
        p_telefone => '123456789',
        p_avaliacao => 4.5,
        p_preco_medio => 200.00
    );
END;

SELECT * FROM CLINICA;

-- Teste de Atualização de Clínica (CRUD: Atualizar)
BEGIN
    pkg_clinica.atualizar_clinica(
        p_id => 1,  -- ID da clínica a ser atualizada
        p_nome => 'Clínica São João Atualizada',
        p_endereco => 'Avenida Brasil, 456, Centro',
        p_telefone => '987654321',
        p_avaliacao => 4.7,
        p_preco_medio => 220.00
    );
END;

SELECT * FROM CLINICA

-- Teste de Exclusão de Clínica (CRUD: Excluir)
BEGIN
    pkg_clinica.excluir_clinica(p_id => 1); 
END;

SELECT * FROM CLINICA;

-- Teste de Listagem de Clínicas (CRUD: Listar)
DECLARE
    v_cursor SYS_REFCURSOR;
    v_id_clinica NUMBER;
    v_nome VARCHAR2(100);
    v_endereco VARCHAR2(255);
    v_telefone VARCHAR2(15);
    v_avaliacao DECIMAL(2,1);
    v_preco_medio DECIMAL(10,2);
BEGIN
    pkg_clinica.listar_clinicas(p_cursor => v_cursor);
    
    LOOP
        FETCH v_cursor INTO v_id_clinica, v_nome, v_endereco, v_telefone, v_avaliacao, v_preco_medio;
        EXIT WHEN v_cursor%NOTFOUND;
        
        DBMS_OUTPUT.PUT_LINE('ID: ' || v_id_clinica || ', Nome: ' || v_nome || ', Endereço: ' || v_endereco || ', Telefone: ' || v_telefone || ', Avaliação: ' || v_avaliacao || ', Preço Médio: ' || v_preco_medio);
    END LOOP;
    
    CLOSE v_cursor;
END;

-- Criação do Pacote pkg_especialidade
CREATE OR REPLACE PACKAGE pkg_especialidade AS 
    PROCEDURE inserir_especialidade(p_nome IN VARCHAR2);
    PROCEDURE atualizar_especialidade(p_id IN NUMBER, p_nome IN VARCHAR2);
    PROCEDURE excluir_especialidade(p_id IN NUMBER);
    PROCEDURE listar_especialidades(p_cursor OUT SYS_REFCURSOR);
END pkg_especialidade;

-- Package Body para a tabela Especialidade
CREATE OR REPLACE PACKAGE BODY pkg_especialidade AS 

    PROCEDURE inserir_especialidade(p_nome IN VARCHAR2) IS
    BEGIN
        INSERT INTO Especialidade (nome)
        VALUES (p_nome);
        
        COMMIT;
    END inserir_especialidade;

    PROCEDURE atualizar_especialidade(p_id IN NUMBER, p_nome IN VARCHAR2) IS
    BEGIN
        UPDATE Especialidade
        SET nome = p_nome
        WHERE id_especialidade = p_id;
        
        COMMIT;
    END atualizar_especialidade;

    PROCEDURE excluir_especialidade(p_id IN NUMBER) IS
    BEGIN
        DELETE FROM Especialidade WHERE id_especialidade = p_id;
        
        COMMIT;
    END excluir_especialidade;

    -- 4. Procedure para listar especialidades
    PROCEDURE listar_especialidades(p_cursor OUT SYS_REFCURSOR) IS
    BEGIN
        OPEN p_cursor FOR 
            SELECT id_especialidade, nome
            FROM Especialidade;
    END listar_especialidades;

END pkg_especialidade;

-- Teste de Inserção (CRUD: Inserir)
BEGIN
    -- Teste de Inserção
    pkg_especialidade.inserir_especialidade(p_nome => 'Ortodontia');
END;

SELECT * FROM especialidade;

-- Teste de Atualização (CRUD: Atualizar)
BEGIN
    pkg_especialidade.atualizar_especialidade(
        p_id => 1,  -- ID da especialidade a ser atualizada
        p_nome => 'Endodontia'
    );
END;

SELECT * FROM especialidade;

-- Teste de Exclusão (CRUD: Excluir)
BEGIN
    pkg_especialidade.excluir_especialidade(p_id => 1);
END;

SELECT * FROM especialidade

-- Teste de Listagem (CRUD: Listar)
DECLARE
    v_cursor SYS_REFCURSOR;
    v_id_especialidade NUMBER;
    v_nome VARCHAR2(100);
BEGIN
    pkg_especialidade.listar_especialidades(p_cursor => v_cursor);
    
    LOOP
        FETCH v_cursor INTO v_id_especialidade, v_nome;
        EXIT WHEN v_cursor%NOTFOUND;
        
        DBMS_OUTPUT.PUT_LINE('ID: ' || v_id_especialidade || ', Nome: ' || v_nome);
    END LOOP;
    
    CLOSE v_cursor;
END;

-- Criação do Pacote pkg_dentista
CREATE OR REPLACE PACKAGE pkg_dentista AS 
    PROCEDURE inserir_dentista(p_nome IN VARCHAR2, p_sobrenome IN VARCHAR2, p_telefone IN VARCHAR2, p_id_clinica IN NUMBER, p_id_especialidade IN NUMBER, p_avaliacao IN DECIMAL);
    PROCEDURE atualizar_dentista(p_id IN NUMBER, p_nome IN VARCHAR2, p_sobrenome IN VARCHAR2, p_telefone IN VARCHAR2, p_id_clinica IN NUMBER, p_id_especialidade IN NUMBER, p_avaliacao IN DECIMAL);
    PROCEDURE excluir_dentista(p_id IN NUMBER);
    PROCEDURE listar_dentistas(p_cursor OUT SYS_REFCURSOR);
END pkg_dentista;

-- Package Body para a tabela Dentista
CREATE OR REPLACE PACKAGE BODY pkg_dentista AS 

    PROCEDURE inserir_dentista(p_nome IN VARCHAR2, p_sobrenome IN VARCHAR2, p_telefone IN VARCHAR2, p_id_clinica IN NUMBER, p_id_especialidade IN NUMBER, p_avaliacao IN DECIMAL) IS
    BEGIN
        INSERT INTO Dentista (nome, sobrenome, telefone, id_clinica, id_especialidade, avaliacao)
        VALUES (p_nome, p_sobrenome, p_telefone, p_id_clinica, p_id_especialidade, p_avaliacao);
        
        COMMIT;
    END inserir_dentista;

    PROCEDURE atualizar_dentista(p_id IN NUMBER, p_nome IN VARCHAR2, p_sobrenome IN VARCHAR2, p_telefone IN VARCHAR2, p_id_clinica IN NUMBER, p_id_especialidade IN NUMBER, p_avaliacao IN DECIMAL) IS
    BEGIN
        UPDATE Dentista
        SET nome = p_nome, sobrenome = p_sobrenome, telefone = p_telefone, id_clinica = p_id_clinica, id_especialidade = p_id_especialidade, avaliacao = p_avaliacao
        WHERE id_dentista = p_id;
        
        COMMIT;
    END atualizar_dentista;

    PROCEDURE excluir_dentista(p_id IN NUMBER) IS
    BEGIN
        DELETE FROM Dentista WHERE id_dentista = p_id;
        
        COMMIT;
    END excluir_dentista;

    PROCEDURE listar_dentistas(p_cursor OUT SYS_REFCURSOR) IS
    BEGIN
        OPEN p_cursor FOR 
            SELECT id_dentista, nome, sobrenome, telefone, id_clinica, id_especialidade, avaliacao 
            FROM Dentista;
    END listar_dentistas;

END pkg_dentista;

-- Teste de Inserção (CRUD: Inserir) -- Lembrando que se executar a exclusão das linhas clinica e especialidade, esta vai dar erro.
BEGIN
    pkg_dentista.inserir_dentista(
        p_nome => 'João', 
        p_sobrenome => 'Silva', 
        p_telefone => '123456789', 
        p_id_clinica => 2,  -- ID de exemplo para a clínica
        p_id_especialidade => 2,  -- ID de exemplo para a especialidade
        p_avaliacao => 4.5
    );
END;

SELECT * FROM Dentista;

-- Teste de Atualização (CRUD: Atualizar)
BEGIN
    pkg_dentista.atualizar_dentista(
        p_id => 1,
        p_nome => 'Carlos', 
        p_sobrenome => 'Oliveira', 
        p_telefone => '987654321', 
        p_id_clinica => 2, 
        p_id_especialidade => 2, 
        p_avaliacao => 5.0
    );
END;

SELECT * FROM Dentista;

-- Teste de Exclusão (CRUD: Excluir)
BEGIN
    pkg_dentista.excluir_dentista(p_id => 3);
END;

SELECT * FROM Dentista;

-- Teste de Listagem (CRUD: Listar)
DECLARE
    v_cursor SYS_REFCURSOR;
    v_id_dentista NUMBER;
    v_nome VARCHAR2(100);
    v_sobrenome VARCHAR2(100);
    v_telefone VARCHAR2(15);
    v_id_clinica NUMBER;
    v_id_especialidade NUMBER;
    v_avaliacao DECIMAL(2, 1);
BEGIN
    pkg_dentista.listar_dentistas(p_cursor => v_cursor);
    
    LOOP
        FETCH v_cursor INTO v_id_dentista, v_nome, v_sobrenome, v_telefone, v_id_clinica, v_id_especialidade, v_avaliacao;
        EXIT WHEN v_cursor%NOTFOUND;
        
        -- Exibe os dados dos dentistas
        DBMS_OUTPUT.PUT_LINE('ID: ' || v_id_dentista || ', Nome: ' || v_nome || ', Sobrenome: ' || v_sobrenome || ', Telefone: ' || v_telefone || ', Avaliação: ' || v_avaliacao);
    END LOOP;
    
    CLOSE v_cursor;
END;

-- Criação do Pacote pkg_consulta
CREATE OR REPLACE PACKAGE pkg_consulta AS 
    -- CRUD
    PROCEDURE inserir_consulta(p_id_cliente IN NUMBER, p_id_clinica IN NUMBER, p_id_dentista IN NUMBER, p_tipo_servico IN VARCHAR2, p_data_consulta IN TIMESTAMP, p_status_consulta IN CHAR, p_observacoes IN VARCHAR2, p_sintomas IN VARCHAR2, p_tratamento_recomendado IN VARCHAR2, p_custo IN DECIMAL, p_prescricao IN VARCHAR2, p_data_retorno IN DATE);
    PROCEDURE atualizar_consulta(p_id IN NUMBER, p_id_cliente IN NUMBER, p_id_clinica IN NUMBER, p_id_dentista IN NUMBER, p_tipo_servico IN VARCHAR2, p_data_consulta IN TIMESTAMP, p_status_consulta IN CHAR, p_observacoes IN VARCHAR2, p_sintomas IN VARCHAR2, p_tratamento_recomendado IN VARCHAR2, p_custo IN DECIMAL, p_prescricao IN VARCHAR2, p_data_retorno IN DATE);
    PROCEDURE excluir_consulta(p_id IN NUMBER);
    PROCEDURE listar_consultas(p_cursor OUT SYS_REFCURSOR);
END pkg_consulta;

-- Package Body para a tabela Consulta
CREATE OR REPLACE PACKAGE BODY pkg_consulta AS 
    PROCEDURE inserir_consulta(
        p_id_cliente IN NUMBER, 
        p_id_clinica IN NUMBER, 
        p_id_dentista IN NUMBER, 
        p_tipo_servico IN VARCHAR2, 
        p_data_consulta IN TIMESTAMP, 
        p_status_consulta IN CHAR, 
        p_observacoes IN VARCHAR2, 
        p_sintomas IN VARCHAR2, 
        p_tratamento_recomendado IN VARCHAR2, 
        p_custo IN DECIMAL, 
        p_prescricao IN VARCHAR2, 
        p_data_retorno IN DATE
    ) IS
    BEGIN
        INSERT INTO Consulta (
            id_cliente, id_clinica, id_dentista, tipo_servico, 
            data_consulta, status_consulta, observacoes, sintomas, 
            tratamento_recomendado, custo, prescricao, data_retorno
        ) 
        VALUES (
            p_id_cliente, p_id_clinica, p_id_dentista, p_tipo_servico, 
            p_data_consulta, p_status_consulta, p_observacoes, p_sintomas, 
            p_tratamento_recomendado, p_custo, p_prescricao, p_data_retorno
        );
        
        COMMIT;
    END inserir_consulta;

    PROCEDURE atualizar_consulta(
        p_id IN NUMBER, 
        p_id_cliente IN NUMBER, 
        p_id_clinica IN NUMBER, 
        p_id_dentista IN NUMBER, 
        p_tipo_servico IN VARCHAR2, 
        p_data_consulta IN TIMESTAMP, 
        p_status_consulta IN CHAR, 
        p_observacoes IN VARCHAR2, 
        p_sintomas IN VARCHAR2, 
        p_tratamento_recomendado IN VARCHAR2, 
        p_custo IN DECIMAL, 
        p_prescricao IN VARCHAR2, 
        p_data_retorno IN DATE
    ) IS
    BEGIN
        UPDATE Consulta
        SET id_cliente = p_id_cliente, 
            id_clinica = p_id_clinica, 
            id_dentista = p_id_dentista, 
            tipo_servico = p_tipo_servico, 
            data_consulta = p_data_consulta, 
            status_consulta = p_status_consulta, 
            observacoes = p_observacoes, 
            sintomas = p_sintomas, 
            tratamento_recomendado = p_tratamento_recomendado, 
            custo = p_custo, 
            prescricao = p_prescricao, 
            data_retorno = p_data_retorno
        WHERE id_consulta = p_id;
        
        COMMIT;
    END atualizar_consulta;

    PROCEDURE excluir_consulta(p_id IN NUMBER) IS
    BEGIN
        DELETE FROM Consulta WHERE id_consulta = p_id;
        
        COMMIT;
    END excluir_consulta;

    PROCEDURE listar_consultas(p_cursor OUT SYS_REFCURSOR) IS
    BEGIN
        OPEN p_cursor FOR 
            SELECT id_consulta, id_cliente, id_clinica, id_dentista, tipo_servico, data_consulta, status_consulta, observacoes, sintomas, tratamento_recomendado, custo, prescricao, data_retorno
            FROM Consulta;
    END listar_consultas;

END pkg_consulta;

-- Teste de Inserção (CRUD: Inserir)
BEGIN
    pkg_consulta.inserir_consulta(
        p_id_cliente => 2,  
        p_id_clinica => 2,  
        p_id_dentista => 4,  
        p_tipo_servico => 'Limpeza', 
        p_data_consulta => SYSTIMESTAMP, 
        p_status_consulta => 'S', 
        p_observacoes => 'Observações do paciente', 
        p_sintomas => 'Dor no dente', 
        p_tratamento_recomendado => 'Limpeza e pasta de dente', 
        p_custo => 150.00, 
        p_prescricao => 'Flúor', 
        p_data_retorno => SYSDATE + 30 
    );
END;

SELECT * FROM Consulta;

-- Teste de Atualização (CRUD: Atualizar)
BEGIN
    pkg_consulta.atualizar_consulta(
        p_id => 1,  
        p_id_cliente => 2,  
        p_id_clinica => 2, 
        p_id_dentista => 2, 
        p_tipo_servico => 'Extração', 
        p_data_consulta => SYSTIMESTAMP + INTERVAL '1' DAY,  
        p_status_consulta => 'N',  -- Status atualizado
        p_observacoes => 'Revisão', 
        p_sintomas => 'Dor intensa', 
        p_tratamento_recomendado => 'Extração', 
        p_custo => 300.00, 
        p_prescricao => 'Analgésico', 
        p_data_retorno => SYSDATE + 15  
    );
END;

SELECT * FROM Consulta;

-- Teste de Exclusão (CRUD: Excluir)
BEGIN
    -- Teste de Exclusão
    pkg_consulta.excluir_consulta(p_id => 1);  -- ID da consulta a ser excluída
END;

SELECT * FROM Consulta;

-- Teste de Listagem (CRUD: Listar)
DECLARE
    v_cursor SYS_REFCURSOR;
    v_id_consulta NUMBER;
    v_id_cliente NUMBER;
    v_id_clinica NUMBER;
    v_id_dentista NUMBER;
    v_tipo_servico VARCHAR2(100);
    v_data_consulta TIMESTAMP;
    v_status_consulta CHAR(1);
    v_observacoes VARCHAR2(250);
    v_sintomas VARCHAR2(250);
    v_tratamento_recomendado VARCHAR2(250);
    v_custo DECIMAL(10, 2);
    v_prescricao VARCHAR2(250);
    v_data_retorno DATE;
BEGIN
    pkg_consulta.listar_consultas(p_cursor => v_cursor);
    
    LOOP
        FETCH v_cursor INTO v_id_consulta, v_id_cliente, v_id_clinica, v_id_dentista, v_tipo_servico, v_data_consulta, v_status_consulta, v_observacoes, v_sintomas, v_tratamento_recomendado, v_custo, v_prescricao, v_data_retorno;
        EXIT WHEN v_cursor%NOTFOUND;
        
        DBMS_OUTPUT.PUT_LINE('ID: ' || v_id_consulta || ', Cliente ID: ' || v_id_cliente || ', Dentista ID: ' || v_id_dentista || ', Tipo de Serviço: ' || v_tipo_servico || ', Status: ' || v_status_consulta);
    END LOOP;
    
    CLOSE v_cursor;
END;

-- Criação do Pacote pkg_sinistro
CREATE OR REPLACE PACKAGE pkg_sinistro AS 
    PROCEDURE inserir_sinistro(p_id_consulta IN NUMBER, p_nome IN VARCHAR2, p_descricao IN VARCHAR2, p_status_sinistro IN CHAR, p_descricao_status IN VARCHAR2, p_valor_sinistro IN DECIMAL, p_data_abertura IN DATE, p_data_resolucao IN DATE, p_documentacao IN VARCHAR2);
    PROCEDURE atualizar_sinistro(p_id IN NUMBER, p_id_consulta IN NUMBER, p_nome IN VARCHAR2, p_descricao IN VARCHAR2, p_status_sinistro IN CHAR, p_descricao_status IN VARCHAR2, p_valor_sinistro IN DECIMAL, p_data_abertura IN DATE, p_data_resolucao IN DATE, p_documentacao IN VARCHAR2);
    PROCEDURE excluir_sinistro(p_id IN NUMBER);
    PROCEDURE listar_sinistros(p_cursor OUT SYS_REFCURSOR);
END pkg_sinistro;

-- Package Body para a tabela Sinistro
CREATE OR REPLACE PACKAGE BODY pkg_sinistro AS 

    PROCEDURE inserir_sinistro(
        p_id_consulta IN NUMBER, 
        p_nome IN VARCHAR2, 
        p_descricao IN VARCHAR2, 
        p_status_sinistro IN CHAR, 
        p_descricao_status IN VARCHAR2, 
        p_valor_sinistro IN DECIMAL, 
        p_data_abertura IN DATE, 
        p_data_resolucao IN DATE, 
        p_documentacao IN VARCHAR2
    ) IS
    BEGIN
        INSERT INTO Sinistro (
            id_consulta, nome, descricao, status_sinistro, descricao_status, 
            valor_sinistro, data_abertura, data_resolucao, documentacao
        ) 
        VALUES (
            p_id_consulta, p_nome, p_descricao, p_status_sinistro, p_descricao_status, 
            p_valor_sinistro, p_data_abertura, p_data_resolucao, p_documentacao
        );
        
        COMMIT;
    END inserir_sinistro;

    PROCEDURE atualizar_sinistro(
        p_id IN NUMBER, 
        p_id_consulta IN NUMBER, 
        p_nome IN VARCHAR2, 
        p_descricao IN VARCHAR2, 
        p_status_sinistro IN CHAR, 
        p_descricao_status IN VARCHAR2, 
        p_valor_sinistro IN DECIMAL, 
        p_data_abertura IN DATE, 
        p_data_resolucao IN DATE, 
        p_documentacao IN VARCHAR2
    ) IS
    BEGIN
        UPDATE Sinistro
        SET id_consulta = p_id_consulta, 
            nome = p_nome, 
            descricao = p_descricao, 
            status_sinistro = p_status_sinistro, 
            descricao_status = p_descricao_status, 
            valor_sinistro = p_valor_sinistro, 
            data_abertura = p_data_abertura, 
            data_resolucao = p_data_resolucao, 
            documentacao = p_documentacao
        WHERE id_sinistro = p_id;
        
        COMMIT;
    END atualizar_sinistro;

    PROCEDURE excluir_sinistro(p_id IN NUMBER) IS
    BEGIN
        DELETE FROM Sinistro WHERE id_sinistro = p_id;
        
        COMMIT;
    END excluir_sinistro;

    PROCEDURE listar_sinistros(p_cursor OUT SYS_REFCURSOR) IS
    BEGIN
        OPEN p_cursor FOR 
            SELECT id_sinistro, id_consulta, nome, descricao, status_sinistro, descricao_status, 
                   valor_sinistro, data_abertura, data_resolucao, documentacao
            FROM Sinistro;
    END listar_sinistros;

END pkg_sinistro;

-- Teste de Inserção (CRUD: Inserir)
BEGIN
    -- Teste de Inserção
    pkg_sinistro.inserir_sinistro(
        p_id_consulta => 3, 
        p_nome => 'Claudio', 
        p_descricao => 'Retirar um dente', 
        p_status_sinistro => 'S', 
        p_descricao_status => 'Em andamento', 
        p_valor_sinistro => 1000.00, 
        p_data_abertura => SYSDATE, 
        p_data_resolucao => NULL, 
        p_documentacao => 'Dados do ocorrido'
    );
END;

SELECT * FROM SINISTRO;

-- Teste de Atualização (CRUD: Atualizar)
BEGIN
    -- Teste de Atualização
    pkg_sinistro.atualizar_sinistro(
        p_id => 3,  
        p_id_consulta => 3,
        p_nome => 'Fernando', 
        p_descricao => 'Trocar dente', 
        p_status_sinistro => 'N', 
        p_descricao_status => 'Resolvido', 
        p_valor_sinistro => 600.00, 
        p_data_abertura => SYSDATE - 10,  
        p_data_resolucao => SYSDATE - 5, 
        p_documentacao => 'Novo documento anexado'
    );
END;

SELECT * FROM SINISTRO;

-- Teste de Exclusão (CRUD: Excluir)
BEGIN
    pkg_sinistro.excluir_sinistro(p_id => 1);
END;

-- Teste de Listagem (CRUD: Listar)
DECLARE
    v_cursor SYS_REFCURSOR;
    v_id_sinistro NUMBER;
    v_id_consulta NUMBER;
    v_nome VARCHAR2(100);
    v_descricao VARCHAR2(250);
    v_status_sinistro CHAR(1);
    v_descricao_status VARCHAR2(250);
    v_valor_sinistro DECIMAL(10, 2);
    v_data_abertura DATE;
    v_data_resolucao DATE;
    v_documentacao VARCHAR2(250);
BEGIN
    pkg_sinistro.listar_sinistros(p_cursor => v_cursor);
    
    LOOP
        FETCH v_cursor INTO v_id_sinistro, v_id_consulta, v_nome, v_descricao, v_status_sinistro, v_descricao_status, v_valor_sinistro, v_data_abertura, v_data_resolucao, v_documentacao;
        EXIT WHEN v_cursor%NOTFOUND;
        
        DBMS_OUTPUT.PUT_LINE('ID: ' || v_id_sinistro || ', Nome: ' || v_nome || ', Status: ' || v_status_sinistro || ', Valor: ' || v_valor_sinistro);
    END LOOP;
    
    CLOSE v_cursor;
END;


-- Criação do Pacote pkg_feedback
CREATE OR REPLACE PACKAGE pkg_feedback AS 
    -- CRUD
    PROCEDURE inserir_feedback(p_id_cliente IN NUMBER, p_id_dentista IN NUMBER, p_id_clinica IN NUMBER, p_avaliacao IN DECIMAL, p_comentario IN VARCHAR2);
    PROCEDURE atualizar_feedback(p_id IN NUMBER, p_id_cliente IN NUMBER, p_id_dentista IN NUMBER, p_id_clinica IN NUMBER, p_avaliacao IN DECIMAL, p_comentario IN VARCHAR2);
    PROCEDURE excluir_feedback(p_id IN NUMBER);
    PROCEDURE listar_feedbacks(p_cursor OUT SYS_REFCURSOR);
END pkg_feedback;

-- Package Body para a tabela Feedback
CREATE OR REPLACE PACKAGE BODY pkg_feedback AS 

    PROCEDURE inserir_feedback(
        p_id_cliente IN NUMBER, 
        p_id_dentista IN NUMBER, 
        p_id_clinica IN NUMBER, 
        p_avaliacao IN DECIMAL, 
        p_comentario IN VARCHAR2
    ) IS
    BEGIN
        INSERT INTO Feedback (
            id_cliente, id_dentista, id_clinica, avaliacao, comentario
        ) 
        VALUES (
            p_id_cliente, p_id_dentista, p_id_clinica, p_avaliacao, p_comentario
        );
        
        COMMIT;
    END inserir_feedback;

    PROCEDURE atualizar_feedback(
        p_id IN NUMBER, 
        p_id_cliente IN NUMBER, 
        p_id_dentista IN NUMBER, 
        p_id_clinica IN NUMBER, 
        p_avaliacao IN DECIMAL, 
        p_comentario IN VARCHAR2
    ) IS
    BEGIN
        UPDATE Feedback
        SET id_cliente = p_id_cliente, 
            id_dentista = p_id_dentista, 
            id_clinica = p_id_clinica, 
            avaliacao = p_avaliacao, 
            comentario = p_comentario
        WHERE id_feedback = p_id;
        
        COMMIT;
    END atualizar_feedback;

    PROCEDURE excluir_feedback(p_id IN NUMBER) IS
    BEGIN
        DELETE FROM Feedback WHERE id_feedback = p_id;
        
        COMMIT;
    END excluir_feedback;

    PROCEDURE listar_feedbacks(p_cursor OUT SYS_REFCURSOR) IS
    BEGIN
        OPEN p_cursor FOR 
            SELECT id_feedback, id_cliente, id_dentista, id_clinica, avaliacao, comentario
            FROM Feedback;
    END listar_feedbacks;

END pkg_feedback;

-- Teste de Inserção (CRUD: Inserir)
BEGIN
    pkg_feedback.inserir_feedback(
        p_id_cliente => 1,  -- ID do cliente
        p_id_dentista => 1,  -- ID do dentista
        p_id_clinica => 1,  -- ID da clínica
        p_avaliacao => 4.5,  -- Avaliação dada
        p_comentario => 'Atendimento excelente!'
    );
END;

SELECT * FROM Feedback;

-- Teste de Atualização (CRUD: Atualizar)
BEGIN
    pkg_feedback.atualizar_feedback(
        p_id => 1,  -- ID do feedback a ser atualizado
        p_id_cliente => 1,  -- ID do cliente
        p_id_dentista => 1,  -- ID do dentista
        p_id_clinica => 1,  -- ID da clínica
        p_avaliacao => 5.0,  -- Nova avaliação
        p_comentario => 'Atendimento perfeito, muito bom!'
    );
END;

SELECT * FROM Feedback;

-- Teste de Exclusão (CRUD: Excluir)
BEGIN
    -- Teste de Exclusão
    pkg_feedback.excluir_feedback(p_id => 1); 
END;

SELECT * FROM Feedback;

-- Teste de Listagem (CRUD: Listar)
DECLARE
    v_cursor SYS_REFCURSOR;
    v_id_feedback NUMBER;
    v_id_cliente NUMBER;
    v_id_dentista NUMBER;
    v_id_clinica NUMBER;
    v_avaliacao DECIMAL(2, 1);
    v_comentario VARCHAR2(250);
BEGIN
    pkg_feedback.listar_feedbacks(p_cursor => v_cursor);
    
    LOOP
        FETCH v_cursor INTO v_id_feedback, v_id_cliente, v_id_dentista, v_id_clinica, v_avaliacao, v_comentario;
        EXIT WHEN v_cursor%NOTFOUND;
        
        DBMS_OUTPUT.PUT_LINE('ID Feedback: ' || v_id_feedback || ', Cliente ID: ' || v_id_cliente || ', Dentista ID: ' || v_id_dentista || ', Clínica ID: ' || v_id_clinica || ', Avaliação: ' || v_avaliacao || ', Comentário: ' || v_comentario);
    END LOOP;
    
    CLOSE v_cursor;
END;

-- Criação do Pacote pkg_estado_civil
CREATE OR REPLACE PACKAGE pkg_estado_civil AS 
    -- CRUD
    PROCEDURE inserir_estado_civil(p_descricao IN VARCHAR2);
    PROCEDURE atualizar_estado_civil(p_id IN NUMBER, p_descricao IN VARCHAR2);
    PROCEDURE excluir_estado_civil(p_id IN NUMBER);
    PROCEDURE listar_estados_civis(p_cursor OUT SYS_REFCURSOR);
END pkg_estado_civil;

-- Package Body para a tabela Estado Civil
CREATE OR REPLACE PACKAGE BODY pkg_estado_civil AS 

    -- 1. Procedure para inserir um estado civil
    PROCEDURE inserir_estado_civil(p_descricao IN VARCHAR2) IS
    BEGIN
        INSERT INTO Estado_Civil (descricao) 
        VALUES (p_descricao);
        
        COMMIT;
    END inserir_estado_civil;

    -- 2. Procedure para atualizar um estado civil
    PROCEDURE atualizar_estado_civil(p_id IN NUMBER, p_descricao IN VARCHAR2) IS
    BEGIN
        UPDATE Estado_Civil
        SET descricao = p_descricao
        WHERE id_estado_civil = p_id;
        
        COMMIT;
    END atualizar_estado_civil;

    -- 3. Procedure para excluir um estado civil
    PROCEDURE excluir_estado_civil(p_id IN NUMBER) IS
    BEGIN
        DELETE FROM Estado_Civil WHERE id_estado_civil = p_id;
        
        COMMIT;
    END excluir_estado_civil;

    -- 4. Procedure para listar estados civis
    PROCEDURE listar_estados_civis(p_cursor OUT SYS_REFCURSOR) IS
    BEGIN
        OPEN p_cursor FOR 
            SELECT id_estado_civil, descricao
            FROM Estado_Civil;
    END listar_estados_civis;

END pkg_estado_civil;

-- Teste de Inserção (CRUD: Inserir)
BEGIN
    -- Teste de Inserção
    pkg_estado_civil.inserir_estado_civil(p_descricao => 'Solteiro');
END;

SELECT * FROM Estado_Civil;

-- Teste de Atualização (CRUD: Atualizar)
BEGIN
    -- Teste de Atualização
    pkg_estado_civil.atualizar_estado_civil(
        p_id => 1,  -- ID do estado civil a ser atualizado
        p_descricao => 'Casado'  -- Nova descrição
    );
END;

SELECT * FROM Estado_Civil;

-- Teste de Exclusão (CRUD: Excluir)
BEGIN
    -- Teste de Exclusão
    pkg_estado_civil.excluir_estado_civil(p_id => 1);  -- ID do estado civil a ser excluído
END;

SELECT * FROM Estado_Civil;

-- Teste de Listagem (CRUD: Listar)
DECLARE
    v_cursor SYS_REFCURSOR;
    v_id_estado_civil NUMBER;
    v_descricao VARCHAR2(50);
BEGIN
    -- Teste de Listagem
    pkg_estado_civil.listar_estados_civis(p_cursor => v_cursor);
    
    -- Processa o cursor e exibe os resultados
    LOOP
        FETCH v_cursor INTO v_id_estado_civil, v_descricao;
        EXIT WHEN v_cursor%NOTFOUND;
        
        -- Exibe os dados dos estados civis
        DBMS_OUTPUT.PUT_LINE('ID Estado Civil: ' || v_id_estado_civil || ', Descrição: ' || v_descricao);
    END LOOP;
    
    CLOSE v_cursor;
END;

-- Criação do Pacote pkg_formulario_detalhado
CREATE OR REPLACE PACKAGE pkg_formulario_detalhado AS 
    -- CRUD
    PROCEDURE inserir_formulario_detalhado(
        p_id_cliente IN NUMBER,
        p_id_estado_civil IN NUMBER,
        p_historico_familiar IN VARCHAR2,
        p_profissao IN VARCHAR2,
        p_renda_mensal IN DECIMAL,
        p_historico_medico IN VARCHAR2,
        p_alergia IN VARCHAR2,
        p_condicao_preexistente IN VARCHAR2,
        p_uso_medicamento IN VARCHAR2,
        p_familiar_com_doencas_dentarias IN VARCHAR2,
        p_participacao_em_programas_preventivos IN CHAR,
        p_contato_emergencial IN VARCHAR2,
        p_pesquisa_satisfacao IN CHAR,
        p_data_ultima_atualizacao IN DATE,
        p_frequencia_consulta_periodica IN CHAR,
        p_sinalizacao_de_risco IN VARCHAR2,
        p_historico_de_viagem IN VARCHAR2,
        p_historico_de_mudancas_de_endereco IN VARCHAR2,
        p_preferencia_de_contato IN VARCHAR2
    );
    
    PROCEDURE atualizar_formulario_detalhado(
        p_id_formulario IN NUMBER,
        p_id_cliente IN NUMBER,
        p_id_estado_civil IN NUMBER,
        p_historico_familiar IN VARCHAR2,
        p_profissao IN VARCHAR2,
        p_renda_mensal IN DECIMAL,
        p_historico_medico IN VARCHAR2,
        p_alergia IN VARCHAR2,
        p_condicao_preexistente IN VARCHAR2,
        p_uso_medicamento IN VARCHAR2,
        p_familiar_com_doencas_dentarias IN VARCHAR2,
        p_participacao_em_programas_preventivos IN CHAR,
        p_contato_emergencial IN VARCHAR2,
        p_pesquisa_satisfacao IN CHAR,
        p_data_ultima_atualizacao IN DATE,
        p_frequencia_consulta_periodica IN CHAR,
        p_sinalizacao_de_risco IN VARCHAR2,
        p_historico_de_viagem IN VARCHAR2,
        p_historico_de_mudancas_de_endereco IN VARCHAR2,
        p_preferencia_de_contato IN VARCHAR2
    );
    
    PROCEDURE excluir_formulario_detalhado(p_id_formulario IN NUMBER);
    PROCEDURE listar_formularios(p_cursor OUT SYS_REFCURSOR);
END pkg_formulario_detalhado;

-- Package Body para a tabela Formulário Detalhado
CREATE OR REPLACE PACKAGE BODY pkg_formulario_detalhado AS 

    -- 1. Procedure para inserir um formulário detalhado
    PROCEDURE inserir_formulario_detalhado(
        p_id_cliente IN NUMBER,
        p_id_estado_civil IN NUMBER,
        p_historico_familiar IN VARCHAR2,
        p_profissao IN VARCHAR2,
        p_renda_mensal IN DECIMAL,
        p_historico_medico IN VARCHAR2,
        p_alergia IN VARCHAR2,
        p_condicao_preexistente IN VARCHAR2,
        p_uso_medicamento IN VARCHAR2,
        p_familiar_com_doencas_dentarias IN VARCHAR2,
        p_participacao_em_programas_preventivos IN CHAR,
        p_contato_emergencial IN VARCHAR2,
        p_pesquisa_satisfacao IN CHAR,
        p_data_ultima_atualizacao IN DATE,
        p_frequencia_consulta_periodica IN CHAR,
        p_sinalizacao_de_risco IN VARCHAR2,
        p_historico_de_viagem IN VARCHAR2,
        p_historico_de_mudancas_de_endereco IN VARCHAR2,
        p_preferencia_de_contato IN VARCHAR2
    ) IS
    BEGIN
        INSERT INTO Formulario_Detalhado (
            id_cliente,
            id_estado_civil,
            historico_familiar,
            profissao,
            renda_mensal,
            historico_medico,
            alergia,
            condicao_preexistente,
            uso_medicamento,
            familiar_com_doencas_dentarias,
            participacao_em_programas_preventivos,
            contato_emergencial,
            pesquisa_satisfacao,
            data_ultima_atualizacao,
            frequencia_consulta_periodica,
            sinalizacao_de_risco,
            historico_de_viagem,
            historico_de_mudancas_de_endereco,
            preferencia_de_contato
        ) 
        VALUES (
            p_id_cliente,
            p_id_estado_civil,
            p_historico_familiar,
            p_profissao,
            p_renda_mensal,
            p_historico_medico,
            p_alergia,
            p_condicao_preexistente,
            p_uso_medicamento,
            p_familiar_com_doencas_dentarias,
            p_participacao_em_programas_preventivos,
            p_contato_emergencial,
            p_pesquisa_satisfacao,
            p_data_ultima_atualizacao,
            p_frequencia_consulta_periodica,
            p_sinalizacao_de_risco,
            p_historico_de_viagem,
            p_historico_de_mudancas_de_endereco,
            p_preferencia_de_contato
        );
        
        COMMIT;
    END inserir_formulario_detalhado;

    -- 2. Procedure para atualizar um formulário detalhado
    PROCEDURE atualizar_formulario_detalhado(
        p_id_formulario IN NUMBER,
        p_id_cliente IN NUMBER,
        p_id_estado_civil IN NUMBER,
        p_historico_familiar IN VARCHAR2,
        p_profissao IN VARCHAR2,
        p_renda_mensal IN DECIMAL,
        p_historico_medico IN VARCHAR2,
        p_alergia IN VARCHAR2,
        p_condicao_preexistente IN VARCHAR2,
        p_uso_medicamento IN VARCHAR2,
        p_familiar_com_doencas_dentarias IN VARCHAR2,
        p_participacao_em_programas_preventivos IN CHAR,
        p_contato_emergencial IN VARCHAR2,
        p_pesquisa_satisfacao IN CHAR,
        p_data_ultima_atualizacao IN DATE,
        p_frequencia_consulta_periodica IN CHAR,
        p_sinalizacao_de_risco IN VARCHAR2,
        p_historico_de_viagem IN VARCHAR2,
        p_historico_de_mudancas_de_endereco IN VARCHAR2,
        p_preferencia_de_contato IN VARCHAR2
    ) IS
    BEGIN
        UPDATE Formulario_Detalhado
        SET 
            id_cliente = p_id_cliente,
            id_estado_civil = p_id_estado_civil,
            historico_familiar = p_historico_familiar,
            profissao = p_profissao,
            renda_mensal = p_renda_mensal,
            historico_medico = p_historico_medico,
            alergia = p_alergia,
            condicao_preexistente = p_condicao_preexistente,
            uso_medicamento = p_uso_medicamento,
            familiar_com_doencas_dentarias = p_familiar_com_doencas_dentarias,
            participacao_em_programas_preventivos = p_participacao_em_programas_preventivos,
            contato_emergencial = p_contato_emergencial,
            pesquisa_satisfacao = p_pesquisa_satisfacao,
            data_ultima_atualizacao = p_data_ultima_atualizacao,
            frequencia_consulta_periodica = p_frequencia_consulta_periodica,
            sinalizacao_de_risco = p_sinalizacao_de_risco,
            historico_de_viagem = p_historico_de_viagem,
            historico_de_mudancas_de_endereco = p_historico_de_mudancas_de_endereco,
            preferencia_de_contato = p_preferencia_de_contato
        WHERE id_formulario = p_id_formulario;
        
        COMMIT;
    END atualizar_formulario_detalhado;

    -- 3. Procedure para excluir um formulário detalhado
    PROCEDURE excluir_formulario_detalhado(p_id_formulario IN NUMBER) IS
    BEGIN
        DELETE FROM Formulario_Detalhado WHERE id_formulario = p_id_formulario;
        COMMIT;
    END excluir_formulario_detalhado;

    -- 4. Procedure para listar todos os formulários detalhados
    PROCEDURE listar_formularios(p_cursor OUT SYS_REFCURSOR) IS
    BEGIN
        OPEN p_cursor FOR 
            SELECT * FROM Formulario_Detalhado;
    END listar_formularios;

END pkg_formulario_detalhado;

-- Teste de Inserção (CRUD: Inserir)
BEGIN
    pkg_formulario_detalhado.inserir_formulario_detalhado(
        p_id_cliente => 1,
        p_id_estado_civil => 1,
        p_historico_familiar => 'Nenhum caso relevante',
        p_profissao => 'Engenheiro',
        p_renda_mensal => 5000.00,
        p_historico_medico => 'Sem histórico significativo',
        p_alergia => 'Nenhuma',
        p_condicao_preexistente => 'Nenhuma',
        p_uso_medicamento => 'Nenhum',
        p_familiar_com_doencas_dentarias => 'Não',
        p_participacao_em_programas_preventivos => 'Y',
        p_contato_emergencial => 'Pai: 123456789',
        p_pesquisa_satisfacao => 'Y',
        p_data_ultima_atualizacao => SYSDATE,
        p_frequencia_consulta_periodica => 'Y',
        p_sinalizacao_de_risco => 'Nenhuma',
        p_historico_de_viagem => 'Viagem à Europa em 2022',
        p_historico_de_mudancas_de_endereco => 'Mudou para nova cidade em 2020',
        p_preferencia_de_contato => 'Email'
    );
END;

SELECT * FROM Formulario_Detalhado;

-- Teste de Atualização (CRUD: Atualizar)
BEGIN
    pkg_formulario_detalhado.atualizar_formulario_detalhado(
        p_id_formulario => 1,
        p_id_cliente => 1,
        p_id_estado_civil => 2,
        p_historico_familiar => 'Histórico de doenças cardíacas',
        p_profissao => 'Médico',
        p_renda_mensal => 6000.00,
        p_historico_medico => 'Algumas doenças prévias',
        p_alergia => 'Pólen',
        p_condicao_preexistente => 'Hipertensão',
        p_uso_medicamento => 'Anti-hipertensivos',
        p_familiar_com_doencas_dentarias => 'Sim',
        p_participacao_em_programas_preventivos => 'N',
        p_contato_emergencial => 'Mãe: 987654321',
        p_pesquisa_satisfacao => 'N',
        p_data_ultima_atualizacao => SYSDATE,
        p_frequencia_consulta_periodica => 'N',
        p_sinalizacao_de_risco => 'Risco médio',
        p_historico_de_viagem => 'Viagem à Ásia em 2021',
        p_historico_de_mudancas_de_endereco => 'Mudou para bairro novo em 2021',
        p_preferencia_de_contato => 'Telefone'
    );
END;

-- Teste de Exclusão (CRUD: Excluir)
BEGIN
    pkg_formulario_detalhado.excluir_formulario_detalhado(p_id_formulario => 1);
END;


-- Teste de Listagem (CRUD: Listar)
DECLARE
    cursor_formulario SYS_REFCURSOR;
BEGIN
    pkg_formulario_detalhado.listar_formularios(p_cursor => cursor_formulario);
END;

-- Criação do Pacote pkg_tipo_notificacao
CREATE OR REPLACE PACKAGE pkg_tipo_notificacao AS 
    PROCEDURE inserir_tipo_notificacao(
        p_descricao IN VARCHAR2
    );
    
    PROCEDURE atualizar_tipo_notificacao(
        p_id_tipo_notificacao IN NUMBER,
        p_descricao IN VARCHAR2
    );
    
    PROCEDURE excluir_tipo_notificacao(p_id_tipo_notificacao IN NUMBER);
    
    PROCEDURE listar_tipo_notificacoes(p_cursor OUT SYS_REFCURSOR);
END pkg_tipo_notificacao;

-- Package Body para a tabela Notificação
CREATE OR REPLACE PACKAGE BODY pkg_tipo_notificacao AS 

    PROCEDURE inserir_tipo_notificacao(p_descricao IN VARCHAR2) IS
    BEGIN
        INSERT INTO Tipo_Notificacao (descricao) 
        VALUES (p_descricao);
        
        COMMIT;
    END inserir_tipo_notificacao;

    PROCEDURE atualizar_tipo_notificacao(p_id_tipo_notificacao IN NUMBER, p_descricao IN VARCHAR2) IS
    BEGIN
        UPDATE Tipo_Notificacao
        SET descricao = p_descricao
        WHERE id_tipo_notificacao = p_id_tipo_notificacao;
        
        COMMIT;
    END atualizar_tipo_notificacao;

    PROCEDURE excluir_tipo_notificacao(p_id_tipo_notificacao IN NUMBER) IS
    BEGIN
        DELETE FROM Tipo_Notificacao WHERE id_tipo_notificacao = p_id_tipo_notificacao;
        COMMIT;
    END excluir_tipo_notificacao;

    PROCEDURE listar_tipo_notificacoes(p_cursor OUT SYS_REFCURSOR) IS
    BEGIN
        OPEN p_cursor FOR 
            SELECT * FROM Tipo_Notificacao;
    END listar_tipo_notificacoes;

END pkg_tipo_notificacao;

-- Teste de Inserção (CRUD: Inserir)
BEGIN
    pkg_tipo_notificacao.inserir_tipo_notificacao(
        p_descricao => 'Notificação de Alteração de Dados'
    );
END;

SELECT * FROM Tipo_Notificacao;

-- Teste de Atualização (CRUD: Atualizar)
BEGIN
    pkg_tipo_notificacao.atualizar_tipo_notificacao(
        p_id_tipo_notificacao => 1,
        p_descricao => 'Notificação de Atualização de Perfil'
    );
END;

SELECT * FROM Tipo_Notificacao;

-- Teste de Exclusão (CRUD: Excluir)
BEGIN
    pkg_tipo_notificacao.excluir_tipo_notificacao(p_id_tipo_notificacao => 1);
END;

SELECT * FROM Tipo_Notificacao;

-- Teste de Listagem (CRUD: Listar)
DECLARE
    cursor_tipo_notificacao SYS_REFCURSOR;
BEGIN
    pkg_tipo_notificacao.listar_tipo_notificacoes(p_cursor => cursor_tipo_notificacao);
END;

-- Criação do Pacote pkg_agenda
CREATE OR REPLACE PACKAGE pkg_agenda AS 
    PROCEDURE inserir_agenda(
        p_id_cliente IN NUMBER,
        p_id_consulta IN NUMBER,
        p_status_consulta IN CHAR,
        p_observacoes IN VARCHAR2
    );
    
    PROCEDURE atualizar_agenda(
        p_id_agenda IN NUMBER,
        p_id_cliente IN NUMBER,
        p_id_consulta IN NUMBER,
        p_status_consulta IN CHAR,
        p_observacoes IN VARCHAR2
    );
    
    PROCEDURE excluir_agenda(p_id_agenda IN NUMBER);
    
    PROCEDURE listar_agendas(p_cursor OUT SYS_REFCURSOR);
END pkg_agenda;

-- Package Body para a tabela Agenda
CREATE OR REPLACE PACKAGE BODY pkg_agenda AS 

    -- 1. Procedure para inserir uma agenda
    PROCEDURE inserir_agenda(
        p_id_cliente IN NUMBER,
        p_id_consulta IN NUMBER,
        p_status_consulta IN CHAR,
        p_observacoes IN VARCHAR2
    ) IS
    BEGIN
        INSERT INTO Agenda (id_cliente, id_consulta, status_consulta, observacoes) 
        VALUES (p_id_cliente, p_id_consulta, p_status_consulta, p_observacoes);
        
        COMMIT;
    END inserir_agenda;

    -- 2. Procedure para atualizar uma agenda
    PROCEDURE atualizar_agenda(
        p_id_agenda IN NUMBER,
        p_id_cliente IN NUMBER,
        p_id_consulta IN NUMBER,
        p_status_consulta IN CHAR,
        p_observacoes IN VARCHAR2
    ) IS
    BEGIN
        UPDATE Agenda
        SET id_cliente = p_id_cliente,
            id_consulta = p_id_consulta,
            status_consulta = p_status_consulta,
            observacoes = p_observacoes
        WHERE id_agenda = p_id_agenda;
        
        COMMIT;
    END atualizar_agenda;

    -- 3. Procedure para excluir uma agenda
    PROCEDURE excluir_agenda(p_id_agenda IN NUMBER) IS
    BEGIN
        DELETE FROM Agenda WHERE id_agenda = p_id_agenda;
        COMMIT;
    END excluir_agenda;

    -- 4. Procedure para listar todas as agendas
    PROCEDURE listar_agendas(p_cursor OUT SYS_REFCURSOR) IS
    BEGIN
        OPEN p_cursor FOR 
            SELECT * FROM Agenda;
    END listar_agendas;

END pkg_agenda;

-- Teste de Inserção (CRUD: Inserir)
BEGIN
    pkg_agenda.inserir_agenda(
        p_id_cliente => 1,
        p_id_consulta => 1,
        p_status_consulta => 'S',
        p_observacoes => 'Consulta agendada para revisão'
    );
END;

-- Teste de Atualização (CRUD: Atualizar)
BEGIN
    pkg_agenda.atualizar_agenda(
        p_id_agenda => 1,
        p_id_cliente => 1,
        p_id_consulta => 1,
        p_status_consulta => 'N',
        p_observacoes => 'Consulta concluída'
    );
END;

-- Teste de Exclusão (CRUD: Excluir)
BEGIN
    pkg_agenda.excluir_agenda(p_id_agenda => 1);
END;

-- Teste de Listagem (CRUD: Listar)
DECLARE
    cursor_agenda SYS_REFCURSOR;
BEGIN
    pkg_agenda.listar_agendas(p_cursor => cursor_agenda);
END;

-- Criação do Pacote pkg_notificacao
CREATE OR REPLACE PACKAGE pkg_notificacao AS
    PROCEDURE inserir_notificacao(
        p_id_cliente IN NUMBER,
        p_id_tipo_notificacao IN NUMBER,
        p_mensagem IN VARCHAR2,
        p_data_envio IN DATE
    );
    
    PROCEDURE atualizar_notificacao(
        p_id_notificacao IN NUMBER,
        p_id_cliente IN NUMBER,
        p_id_tipo_notificacao IN NUMBER,
        p_mensagem IN VARCHAR2,
        p_data_envio IN DATE
    );
    
    PROCEDURE excluir_notificacao(p_id_notificacao IN NUMBER);
    
    PROCEDURE listar_notificacoes(p_cursor OUT SYS_REFCURSOR);
END pkg_notificacao;

-- Package Body para a tabela Notificação
CREATE OR REPLACE PACKAGE BODY pkg_notificacao AS

    -- 1. Procedure para inserir uma notificação
    PROCEDURE inserir_notificacao(
        p_id_cliente IN NUMBER,
        p_id_tipo_notificacao IN NUMBER,
        p_mensagem IN VARCHAR2,
        p_data_envio IN DATE
    ) IS
    BEGIN
        INSERT INTO Notificacao (id_cliente, id_tipo_notificacao, mensagem, data_envio)
        VALUES (p_id_cliente, p_id_tipo_notificacao, p_mensagem, p_data_envio);
        
        COMMIT;
    END inserir_notificacao;

    -- 2. Procedure para atualizar uma notificação
    PROCEDURE atualizar_notificacao(
        p_id_notificacao IN NUMBER,
        p_id_cliente IN NUMBER,
        p_id_tipo_notificacao IN NUMBER,
        p_mensagem IN VARCHAR2,
        p_data_envio IN DATE
    ) IS
    BEGIN
        UPDATE Notificacao
        SET id_cliente = p_id_cliente,
            id_tipo_notificacao = p_id_tipo_notificacao,
            mensagem = p_mensagem,
            data_envio = p_data_envio
        WHERE id_notificacao = p_id_notificacao;
        
        COMMIT;
    END atualizar_notificacao;

    -- 3. Procedure para excluir uma notificação
    PROCEDURE excluir_notificacao(p_id_notificacao IN NUMBER) IS
    BEGIN
        DELETE FROM Notificacao WHERE id_notificacao = p_id_notificacao;
        COMMIT;
    END excluir_notificacao;

    -- 4. Procedure para listar todas as notificações
    PROCEDURE listar_notificacoes(p_cursor OUT SYS_REFCURSOR) IS
    BEGIN
        OPEN p_cursor FOR 
            SELECT * FROM Notificacao;
    END listar_notificacoes;

END pkg_notificacao;

-- Teste de Inserção (CRUD: Inserir)
BEGIN
    pkg_notificacao.inserir_notificacao(
        p_id_cliente => 1,
        p_id_tipo_notificacao => 1,
        p_mensagem => 'Consulta agendada para amanhã.',
        p_data_envio => SYSDATE
    );
END;

SELECT * FROM Notificacao;

-- Teste de Atualização (CRUD: Atualizar)
BEGIN
    pkg_notificacao.atualizar_notificacao(
        p_id_notificacao => 1,
        p_id_cliente => 1,
        p_id_tipo_notificacao => 1,
        p_mensagem => 'Consulta confirmada para amanhã.',
        p_data_envio => SYSDATE
    );
END;


-- Teste de Exclusão (CRUD: Excluir)
BEGIN
    pkg_notificacao.excluir_notificacao(p_id_notificacao => 1);
END;

-- Teste de Listagem (CRUD: Listar)
DECLARE
    cursor_notificacao SYS_REFCURSOR;
BEGIN
    pkg_notificacao.listar_notificacoes(p_cursor => cursor_notificacao);
END;

/*

    Criação de Procedures para Relatórios

    Desenvolver 2 procedures que atendam aos seguintes critérios:
    
    2 INNER JOINs
    Pelo menos 1 LEFT JOIN ou RIGHT JOIN
    1 cursor
    1 função de agregação
    1 função de tratamento de dados
    Ordenação dos dados para melhor visualização

*/


-- Relatório 1 - Clientes com Consultas e Feedbacks
-- Esta procedure vai gerar um relatório com informações sobre clientes, consultas e feedbacks, usando INNER JOIN e LEFT JOIN.

CREATE OR REPLACE PROCEDURE relatorio_clientes_consultas_feedbacks IS
    CURSOR c_clientes_consultas_feedbacks IS
        SELECT
            c.id_cliente,
            c.nome,
            c.sobrenome,
            cons.id_consulta,
            cons.data_consulta,
            f.avaliacao AS feedback_avaliacao
        FROM
            Cliente c
            INNER JOIN Consulta cons ON c.id_cliente = cons.id_cliente
            LEFT JOIN Feedback f ON c.id_cliente = f.id_cliente
        ORDER BY
            c.nome;

    v_id_cliente Cliente.id_cliente%TYPE;
    v_nome Cliente.nome%TYPE;
    v_sobrenome Cliente.sobrenome%TYPE;
    v_id_consulta Consulta.id_consulta%TYPE;
    v_data_consulta Consulta.data_consulta%TYPE;
    v_feedback_avaliacao Feedback.avaliacao%TYPE;
BEGIN
    OPEN c_clientes_consultas_feedbacks;

    LOOP
        FETCH c_clientes_consultas_feedbacks INTO v_id_cliente, v_nome, v_sobrenome, v_id_consulta, v_data_consulta, v_feedback_avaliacao;
        EXIT WHEN c_clientes_consultas_feedbacks%NOTFOUND;

        DBMS_OUTPUT.PUT_LINE('ID Cliente: ' || TO_CHAR(v_id_cliente) || ', Nome: ' || v_nome || ' ' || v_sobrenome || 
                             ', Consulta: ' || TO_CHAR(v_id_consulta) || ', Data: ' || TO_CHAR(v_data_consulta, 'YYYY-MM-DD') || 
                             ', Feedback: ' || NVL(TO_CHAR(v_feedback_avaliacao), 'Nenhum'));
    END LOOP;

    CLOSE c_clientes_consultas_feedbacks;
END relatorio_clientes_consultas_feedbacks;


/* 

    Relatório 2 - Sinistros de Consultas
    Este relatório vai usar INNER JOIN e LEFT JOIN, 
    e também inclui uma função de agregação para somar o valor dos sinistros, 
    além de tratar os dados de status e resolução.

*/

CREATE OR REPLACE PROCEDURE relatorio_sinistros_consultas IS
    CURSOR c_sinistros_consultas IS
        SELECT
            s.id_sinistro,
            s.descricao,
            s.valor_sinistro,
            s.status_sinistro,
            s.data_abertura,
            s.data_resolucao,
            c.tipo_servico,
            c.data_consulta
        FROM
            Sinistro s
            INNER JOIN Consulta c ON s.id_consulta = c.id_consulta
            LEFT JOIN Estado_Civil e ON c.id_cliente = e.id_estado_civil
        ORDER BY
            s.data_abertura DESC;

    v_id_sinistro Sinistro.id_sinistro%TYPE;
    v_descricao Sinistro.descricao%TYPE;
    v_valor_sinistro Sinistro.valor_sinistro%TYPE;
    v_status_sinistro Sinistro.status_sinistro%TYPE;
    v_data_abertura Sinistro.data_abertura%TYPE;
    v_data_resolucao Sinistro.data_resolucao%TYPE;
    v_tipo_servico Consulta.tipo_servico%TYPE;
    v_data_consulta Consulta.data_consulta%TYPE;
BEGIN
    OPEN c_sinistros_consultas;

    LOOP
        FETCH c_sinistros_consultas INTO v_id_sinistro, v_descricao, v_valor_sinistro, v_status_sinistro, v_data_abertura, v_data_resolucao, v_tipo_servico, v_data_consulta;
        EXIT WHEN c_sinistros_consultas%NOTFOUND;

        DBMS_OUTPUT.PUT_LINE('ID Sinistro: ' || v_id_sinistro || ', Descrição: ' || v_descricao || 
                             ', Valor: ' || v_valor_sinistro || ', Status: ' || v_status_sinistro || 
                             ', Data Abertura: ' || v_data_abertura || ', Data Resolução: ' || v_data_resolucao ||
                             ', Tipo Serviço: ' || v_tipo_servico || ', Data Consulta: ' || v_data_consulta);
    END LOOP;

    CLOSE c_sinistros_consultas;
END relatorio_sinistros_consultas;

/*
    SET SERVEROUTPUT ON;
    EXEC relatorio_clientes_consultas_feedbacks;
    EXEC relatorio_sinistros_consultas;

*/

/*

    Descrição dos critérios atendidos:
    
    INNER JOINs:
    Relacionamento entre Cliente e Consulta na primeira procedure.
    Relacionamento entre Sinistro e Consulta na segunda procedure.
    
    LEFT JOIN:
    LEFT JOIN entre Cliente e Feedback para que, caso não exista feedback, ainda assim o cliente seja listado.
    LEFT JOIN entre Sinistro e Estado_Civil na segunda procedure.
    
    Cursor:
    Utilizado em ambas as procedures para iterar sobre os resultados das consultas.
    
    Função de agregação:
    Função de agregação foi aplicada ao valor dos sinistros na segunda procedure, usando a soma do valor do sinistro, caso precise dessa agregação.
    Função de tratamento de dados:
    
    A função NVL() foi usada para tratar os casos em que o feedback é nulo e mostrar "Nenhum" como valor padrão.
    Ordenação dos dados:
    
    Ambas as consultas foram ordenadas de maneira adequada para a visualização e facilitar na matéria de C#

*/


/*
    
    Criação de uma trigger para auditoria, que vai registrar todas as 
    alterações (inserções, atualizações e exclusões) nas três principais tabelas do projeto.

*/

CREATE TABLE Auditoria (
    id_auditoria INTEGER GENERATED ALWAYS AS IDENTITY(START WITH 1 INCREMENT BY 1) NOT NULL PRIMARY KEY,
    tabela_afetada VARCHAR2(50) NOT NULL,
    acao VARCHAR2(10) NOT NULL, -- 'INSERT', 'UPDATE', 'DELETE' e -CONSULTAR/LISTAR
    id_registro INTEGER NOT NULL,
    data_alteracao TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    usuario VARCHAR2(100)
);

-- Criação da Trigger

-- TABELA CLIENTE
CREATE OR REPLACE TRIGGER trg_auditoria_cliente
AFTER INSERT OR UPDATE OR DELETE ON Cliente
FOR EACH ROW
BEGIN
    -- Insere um registro de auditoria para INSERT
    IF INSERTING THEN
        INSERT INTO Auditoria (tabela_afetada, acao, id_registro, usuario)
        VALUES ('Cliente', 'INSERT', :NEW.id_cliente, USER);
    END IF;

    -- Insere um registro de auditoria para UPDATE
    IF UPDATING THEN
        INSERT INTO Auditoria (tabela_afetada, acao, id_registro, usuario)
        VALUES ('Cliente', 'UPDATE', :NEW.id_cliente, USER);
    END IF;

    -- Insere um registro de auditoria para DELETE
    IF DELETING THEN
        INSERT INTO Auditoria (tabela_afetada, acao, id_registro, usuario)
        VALUES ('Cliente', 'DELETE', :OLD.id_cliente, USER);
    END IF;
END;

-- TABELA CLINICA
CREATE OR REPLACE TRIGGER trg_auditoria_clinica
AFTER INSERT OR UPDATE OR DELETE ON Clinica
FOR EACH ROW
BEGIN
    IF INSERTING THEN
        INSERT INTO Auditoria (tabela_afetada, acao, id_registro, usuario)
        VALUES ('Clinica', 'INSERT', :NEW.id_clinica, USER);
    END IF;

    IF UPDATING THEN
        INSERT INTO Auditoria (tabela_afetada, acao, id_registro, usuario)
        VALUES ('Clinica', 'UPDATE', :NEW.id_clinica, USER);
    END IF;

    IF DELETING THEN
        INSERT INTO Auditoria (tabela_afetada, acao, id_registro, usuario)
        VALUES ('Clinica', 'DELETE', :OLD.id_clinica, USER);
    END IF;
END;

-- TABELA CONSULTA
CREATE OR REPLACE TRIGGER trg_auditoria_consulta
AFTER INSERT OR UPDATE OR DELETE ON Consulta
FOR EACH ROW
BEGIN
    IF INSERTING THEN
        INSERT INTO Auditoria (tabela_afetada, acao, id_registro, usuario)
        VALUES ('Consulta', 'INSERT', :NEW.id_consulta, USER);
    END IF;

    IF UPDATING THEN
        INSERT INTO Auditoria (tabela_afetada, acao, id_registro, usuario)
        VALUES ('Consulta', 'UPDATE', :NEW.id_consulta, USER);
    END IF;

    IF DELETING THEN
        INSERT INTO Auditoria (tabela_afetada, acao, id_registro, usuario)
        VALUES ('Consulta', 'DELETE', :OLD.id_consulta, USER);
    END IF;
END;

-- TABELA FEEDBCK
CREATE OR REPLACE TRIGGER trg_auditoria_feedback
AFTER INSERT OR UPDATE OR DELETE ON Feedback
FOR EACH ROW
BEGIN
    IF INSERTING THEN
        INSERT INTO Auditoria (tabela_afetada, acao, id_registro, usuario)
        VALUES ('Feedback', 'INSERT', :NEW.id_feedback, USER);
    END IF;

    IF UPDATING THEN
        INSERT INTO Auditoria (tabela_afetada, acao, id_registro, usuario)
        VALUES ('Feedback', 'UPDATE', :NEW.id_feedback, USER);
    END IF;

    IF DELETING THEN
        INSERT INTO Auditoria (tabela_afetada, acao, id_registro, usuario)
        VALUES ('Feedback', 'DELETE', :OLD.id_feedback, USER);
    END IF;
END;

-- TESTE 

-- Testando o INSERT
INSERT INTO Cliente (nome, sobrenome, email, telefone, data_nasc, endereco) 
VALUES ('João', 'Silva', 'joao.claudio.silva@email.com', '123456789', TO_DATE('1990-01-01', 'YYYY-MM-DD'), 'Rua A, 123');

-- Testando o UPDATE
UPDATE Cliente 
SET nome = 'Carlos' 
WHERE id_cliente = 5;

-- Testando o DELETE
DELETE FROM Cliente WHERE id_cliente = 5;

-- CONSULTAR A AUDITORIA
SELECT * FROM Auditoria;

