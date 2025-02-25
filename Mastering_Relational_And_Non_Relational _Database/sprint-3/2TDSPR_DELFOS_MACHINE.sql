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

// Precisamos dos inserts para validar algumas Procedures, pois elas possuem chaves estrangeiras

-- Inserts Cliente

INSERT INTO Cliente (nome, sobrenome, email, telefone, data_nasc, endereco) VALUES
('Alice', 'Silva', 'alice.silva@example.com', '11987654321', TO_DATE('1990-01-15', 'YYYY-MM-DD'), 'Rua das Flores, 123');

INSERT INTO Cliente (nome, sobrenome, email, telefone, data_nasc, endereco) VALUES
('Bruno', 'Santos', 'bruno.santos@example.com', '11976543210', TO_DATE('1985-03-20', 'YYYY-MM-DD'), 'Avenida Central, 456');

INSERT INTO Cliente (nome, sobrenome, email, telefone, data_nasc, endereco) VALUES
('Carlos', 'Oliveira', 'carlos.oliveira@example.com', '11965432109', TO_DATE('1992-07-11', 'YYYY-MM-DD'), 'Rua dos Lírios, 789');

INSERT INTO Cliente (nome, sobrenome, email, telefone, data_nasc, endereco) VALUES
('Diana', 'Pereira', 'diana.pereira@example.com', '11954321098', TO_DATE('1988-04-22', 'YYYY-MM-DD'), 'Praça das Árvores, 321');

INSERT INTO Cliente (nome, sobrenome, email, telefone, data_nasc, endereco) VALUES
('Eduardo', 'Melo', 'eduardo.melo@example.com', '11943210987', TO_DATE('1980-09-05', 'YYYY-MM-DD'), 'Avenida do Sol, 654');

INSERT INTO Cliente (nome, sobrenome, email, telefone, data_nasc, endereco) VALUES
('Fernanda', 'Costa', 'fernanda.costa@example.com', '11932109876', TO_DATE('1995-12-30', 'YYYY-MM-DD'), 'Rua das Estrelas, 852');

INSERT INTO Cliente (nome, sobrenome, email, telefone, data_nasc, endereco) VALUES
('Gabriel', 'Santos', 'gabriel.santos@example.com', '11921098765', TO_DATE('1991-11-19', 'YYYY-MM-DD'), 'Rua das Palmeiras, 963');

INSERT INTO Cliente (nome, sobrenome, email, telefone, data_nasc, endereco) VALUES
('Helena', 'Lima', 'helena.lima@example.com', '11910987654', TO_DATE('1989-06-15', 'YYYY-MM-DD'), 'Avenida das Rosas, 147');

INSERT INTO Cliente (nome, sobrenome, email, telefone, data_nasc, endereco) VALUES
('Igor', 'Barros', 'igor.barros@example.com', '11898765432', TO_DATE('1994-03-29', 'YYYY-MM-DD'), 'Rua das Acácias, 258');

INSERT INTO Cliente (nome, sobrenome, email, telefone, data_nasc, endereco) VALUES
('Juliana', 'Martins', 'juliana.martins@example.com', '11787654321', TO_DATE('1993-08-10', 'YYYY-MM-DD'), 'Avenida da Paz, 369');

select * from Cliente;

-- Clinica

INSERT INTO Clinica (nome, endereco, telefone, avaliacao, preco_medio) VALUES ('Clínica Saúde e Sorriso', 'Rua das Flores, 123', '11987654321', 4.5, 150.00);
INSERT INTO Clinica (nome, endereco, telefone, avaliacao, preco_medio) VALUES ('Clínica Odontológica São José', 'Avenida Central, 456', '11976543210', 4.0, 200.00);
INSERT INTO Clinica (nome, endereco, telefone, avaliacao, preco_medio) VALUES ('Clínica Dental Plus', 'Rua da Esperança, 789', '11965432109', 5.0, 250.00);
INSERT INTO Clinica (nome, endereco, telefone, avaliacao, preco_medio) VALUES ('OdontoCare', 'Avenida Brasil, 321', '11954321098', 3.8, 180.00);
INSERT INTO Clinica (nome, endereco, telefone, avaliacao, preco_medio) VALUES ('Clínica do Sorriso', 'Rua das Acácias, 654', '11943210987', 4.2, 170.00);
INSERT INTO Clinica (nome, endereco, telefone, avaliacao, preco_medio) VALUES ('Clínica OdontoMax', 'Praça da Saúde, 135', '11932109876', 4.7, 220.00);
INSERT INTO Clinica (nome, endereco, telefone, avaliacao, preco_medio) VALUES ('Centro Dental', 'Alameda dos Anjos, 246', '11921098765', 4.1, 160.00);
INSERT INTO Clinica (nome, endereco, telefone, avaliacao, preco_medio) VALUES ('Clínica Vida e Sorriso', 'Rua do Progresso, 357', '11910987654', 4.6, 190.00);
INSERT INTO Clinica (nome, endereco, telefone, avaliacao, preco_medio) VALUES ('Consultório Odontológico Esperança', 'Beco do Sorriso, 888', '11909876543', 3.9, 175.00);
INSERT INTO Clinica (nome, endereco, telefone, avaliacao, preco_medio) VALUES ('Clínica Nova Esperança', 'Rua do Futuro, 101', '11998765432', 4.8, 210.00);

select * from Clinica;

-- Especialidade

INSERT INTO Especialidade (nome) VALUES ('Ortodontia');
INSERT INTO Especialidade (nome) VALUES ('Implantodontia');
INSERT INTO Especialidade (nome) VALUES ('Odontopediatria');
INSERT INTO Especialidade (nome) VALUES ('Periodontia');
INSERT INTO Especialidade (nome) VALUES ('Endodontia');
INSERT INTO Especialidade (nome) VALUES ('Odontologia Estética');
INSERT INTO Especialidade (nome) VALUES ('Cirurgia Bucomaxilofacial');
INSERT INTO Especialidade (nome) VALUES ('Prótese Dentária');
INSERT INTO Especialidade (nome) VALUES ('Dentística');
INSERT INTO Especialidade (nome) VALUES ('Higiene Dental');

select * from Especialidade;

-- Dentista

INSERT INTO Dentista (nome, sobrenome, telefone, id_clinica, id_especialidade, avaliacao) VALUES ('Carlos', 'Menezes', '11987654321', 1, 1, 4.5);
INSERT INTO Dentista (nome, sobrenome, telefone, id_clinica, id_especialidade, avaliacao) VALUES ('Fernanda', 'Pereira', '11976543210', 1, 2, 5.0);
INSERT INTO Dentista (nome, sobrenome, telefone, id_clinica, id_especialidade, avaliacao) VALUES ('Roberto', 'Almeida', '11965432109', 2, 3, 4.0);
INSERT INTO Dentista (nome, sobrenome, telefone, id_clinica, id_especialidade, avaliacao) VALUES ('Mariana', 'Souza', '11954321098', 2, 4, 4.7);
INSERT INTO Dentista (nome, sobrenome, telefone, id_clinica, id_especialidade, avaliacao) VALUES ('André', 'Lima', '11943210987', 1, 5, 3.5);
INSERT INTO Dentista (nome, sobrenome, telefone, id_clinica, id_especialidade, avaliacao) VALUES ('Juliana', 'Silva', '11932109876', 3, 6, 4.8);
INSERT INTO Dentista (nome, sobrenome, telefone, id_clinica, id_especialidade, avaliacao) VALUES ('Ricardo', 'Santos', '11921098765', 3, 7, 4.2);
INSERT INTO Dentista (nome, sobrenome, telefone, id_clinica, id_especialidade, avaliacao) VALUES ('Aline', 'Costa', '11910987654', 1, 8, 4.6);
INSERT INTO Dentista (nome, sobrenome, telefone, id_clinica, id_especialidade, avaliacao) VALUES ('Felipe', 'Martins', '11909876543', 2, 9, 3.9);
INSERT INTO Dentista (nome, sobrenome, telefone, id_clinica, id_especialidade, avaliacao) VALUES ('Tatiane', 'Ferreira', '11998765432', 3, 10, 5.0);

select * from Dentista;

-- Consulta

INSERT INTO Consulta (id_cliente, id_clinica, id_dentista, tipo_servico, data_consulta, status_consulta, observacoes, sintomas, tratamento_recomendado, custo, prescricao, data_retorno) VALUES (1, 1, 1, 'Check-up', TO_TIMESTAMP('2024-10-01 10:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'S', 'Consulta de rotina', 'Nenhum', 'Manter higiene bucal', 150.00, 'Fluoretação', TO_DATE('2024-10-15', 'YYYY-MM-DD'));
INSERT INTO Consulta (id_cliente, id_clinica, id_dentista, tipo_servico, data_consulta, status_consulta, observacoes, sintomas, tratamento_recomendado, custo, prescricao, data_retorno) VALUES (2, 1, 1, 'Limpeza', TO_TIMESTAMP('2024-10-02 11:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'S', 'Limpeza dental regular', 'Acúmulo de tártaro', 'Limpeza profunda', 200.00, 'Nenhum', TO_DATE('2024-10-16', 'YYYY-MM-DD'));
INSERT INTO Consulta (id_cliente, id_clinica, id_dentista, tipo_servico, data_consulta, status_consulta, observacoes, sintomas, tratamento_recomendado, custo, prescricao, data_retorno) VALUES (3, 2, 2, 'Consulta de emergência', TO_TIMESTAMP('2024-10-03 14:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'S', 'Dor de dente', 'Dor intensa', 'Tratamento de canal', 500.00, 'Analgésicos', TO_DATE('2024-10-20', 'YYYY-MM-DD'));
INSERT INTO Consulta (id_cliente, id_clinica, id_dentista, tipo_servico, data_consulta, status_consulta, observacoes, sintomas, tratamento_recomendado, custo, prescricao, data_retorno) VALUES (4, 2, 2, 'Check-up', TO_TIMESTAMP('2024-10-04 09:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'S', 'Consulta de rotina', 'Sem queixas', 'Manter cuidados', 150.00, 'Fluoretação', TO_DATE('2024-10-18', 'YYYY-MM-DD'));
INSERT INTO Consulta (id_cliente, id_clinica, id_dentista, tipo_servico, data_consulta, status_consulta, observacoes, sintomas, tratamento_recomendado, custo, prescricao, data_retorno) VALUES (5, 3, 3, 'Limpeza', TO_TIMESTAMP('2024-10-05 16:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'N', 'Limpeza programada', 'Alguma dor', 'Limpeza e acompanhamento', 220.00, 'Analgésicos', TO_DATE('2024-10-22', 'YYYY-MM-DD'));
INSERT INTO Consulta (id_cliente, id_clinica, id_dentista, tipo_servico, data_consulta, status_consulta, observacoes, sintomas, tratamento_recomendado, custo, prescricao, data_retorno) VALUES (6, 3, 3, 'Consulta de emergência', TO_TIMESTAMP('2024-10-06 15:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'S', 'Dor na gengiva', 'Dor constante', 'Antibióticos', 300.00, 'Antibióticos', TO_DATE('2024-10-23', 'YYYY-MM-DD'));
INSERT INTO Consulta (id_cliente, id_clinica, id_dentista, tipo_servico, data_consulta, status_consulta, observacoes, sintomas, tratamento_recomendado, custo, prescricao, data_retorno) VALUES (7, 1, 1, 'Check-up', TO_TIMESTAMP('2024-10-07 13:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'S', 'Consulta de rotina', 'Nenhum', 'Manter higiene bucal', 150.00, 'Fluoretação', TO_DATE('2024-10-24', 'YYYY-MM-DD'));
INSERT INTO Consulta (id_cliente, id_clinica, id_dentista, tipo_servico, data_consulta, status_consulta, observacoes, sintomas, tratamento_recomendado, custo, prescricao, data_retorno) VALUES (8, 1, 1, 'Limpeza', TO_TIMESTAMP('2024-10-08 10:30:00', 'YYYY-MM-DD HH24:MI:SS'), 'N', 'Limpeza agendada', 'Acúmulo de tártaro', 'Limpeza profunda', 200.00, 'Nenhum', TO_DATE('2024-10-25', 'YYYY-MM-DD'));
INSERT INTO Consulta (id_cliente, id_clinica, id_dentista, tipo_servico, data_consulta, status_consulta, observacoes, sintomas, tratamento_recomendado, custo, prescricao, data_retorno) VALUES (9, 2, 2, 'Consulta de emergência', TO_TIMESTAMP('2024-10-09 14:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'S', 'Dor de dente', 'Dor intensa', 'Tratamento de canal', 500.00, 'Analgésicos', TO_DATE('2024-10-30', 'YYYY-MM-DD'));
INSERT INTO Consulta (id_cliente, id_clinica, id_dentista, tipo_servico, data_consulta, status_consulta, observacoes, sintomas, tratamento_recomendado, custo, prescricao, data_retorno) VALUES (10, 3, 3, 'Check-up', TO_TIMESTAMP('2024-10-10 11:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'S', 'Consulta de rotina', 'Sem queixas', 'Manter cuidados', 150.00, 'Fluoretação', TO_DATE('2024-10-31', 'YYYY-MM-DD'));

select * from Consulta;

-- Sinistro

INSERT INTO Sinistro (id_consulta, nome, descricao, status_sinistro, descricao_status, valor_sinistro, data_abertura, data_resolucao, documentacao) VALUES (1, 'Sinistro 1', 'Quebra de dente durante a consulta', 'S', 'Aguardando documentação', 1000.00, TO_DATE('2024-10-01', 'YYYY-MM-DD'), NULL, 'documento1.pdf');
INSERT INTO Sinistro (id_consulta, nome, descricao, status_sinistro, descricao_status, valor_sinistro, data_abertura, data_resolucao, documentacao) VALUES (2, 'Sinistro 2', 'Reação alérgica após tratamento', 'S', 'Em análise', 800.00, TO_DATE('2024-10-02', 'YYYY-MM-DD'), NULL, 'documento2.pdf');
INSERT INTO Sinistro (id_consulta, nome, descricao, status_sinistro, descricao_status, valor_sinistro, data_abertura, data_resolucao, documentacao) VALUES (3, 'Sinistro 3', 'Dor intensa após procedimento', 'N', 'Resolvido', 500.00, TO_DATE('2024-10-03', 'YYYY-MM-DD'), TO_DATE('2024-10-10', 'YYYY-MM-DD'), 'documento3.pdf');
INSERT INTO Sinistro (id_consulta, nome, descricao, status_sinistro, descricao_status, valor_sinistro, data_abertura, data_resolucao, documentacao) VALUES (4, 'Sinistro 4', 'Infeção após extração', 'S', 'Aguardando avaliação', 1200.00, TO_DATE('2024-10-04', 'YYYY-MM-DD'), NULL, 'documento4.pdf');
INSERT INTO Sinistro (id_consulta, nome, descricao, status_sinistro, descricao_status, valor_sinistro, data_abertura, data_resolucao, documentacao) VALUES (5, 'Sinistro 5', 'Lesão na gengiva', 'S', 'Aguardando resposta do dentista', 300.00, TO_DATE('2024-10-05', 'YYYY-MM-DD'), NULL, 'documento5.pdf');
INSERT INTO Sinistro (id_consulta, nome, descricao, status_sinistro, descricao_status, valor_sinistro, data_abertura, data_resolucao, documentacao) VALUES (6, 'Sinistro 6', 'Complicação com anestesia', 'N', 'Resolvido', 600.00, TO_DATE('2024-10-06', 'YYYY-MM-DD'), TO_DATE('2024-10-11', 'YYYY-MM-DD'), 'documento6.pdf');
INSERT INTO Sinistro (id_consulta, nome, descricao, status_sinistro, descricao_status, valor_sinistro, data_abertura, data_resolucao, documentacao) VALUES (7, 'Sinistro 7', 'Queixa de dor persistente', 'S', 'Em avaliação', 400.00, TO_DATE('2024-10-07', 'YYYY-MM-DD'), NULL, 'documento7.pdf');
INSERT INTO Sinistro (id_consulta, nome, descricao, status_sinistro, descricao_status, valor_sinistro, data_abertura, data_resolucao, documentacao) VALUES (8, 'Sinistro 8', 'Tratamento de canal falhou', 'S', 'Aguardando revisão', 900.00, TO_DATE('2024-10-08', 'YYYY-MM-DD'), NULL, 'documento8.pdf');
INSERT INTO Sinistro (id_consulta, nome, descricao, status_sinistro, descricao_status, valor_sinistro, data_abertura, data_resolucao, documentacao) VALUES (9, 'Sinistro 9', 'Problema na prótese', 'N', 'Resolvido', 700.00, TO_DATE('2024-10-09', 'YYYY-MM-DD'), TO_DATE('2024-10-15', 'YYYY-MM-DD'), 'documento9.pdf');
INSERT INTO Sinistro (id_consulta, nome, descricao, status_sinistro, descricao_status, valor_sinistro, data_abertura, data_resolucao, documentacao) VALUES (10, 'Sinistro 10', 'Queixa de mau hálito persistente', 'S', 'Aguardando consulta adicional', 200.00, TO_DATE('2024-10-10', 'YYYY-MM-DD'), NULL, 'documento10.pdf');

select * from Sinistro;

-- Feedback

INSERT INTO Feedback (id_cliente, id_dentista, id_clinica, avaliacao, comentario) VALUES (1, 1, 1, 5, 'Ótimo atendimento!');
INSERT INTO Feedback (id_cliente, id_dentista, id_clinica, avaliacao, comentario) VALUES (2, 1, 1, 4, 'A consulta foi muito boa.');
INSERT INTO Feedback (id_cliente, id_dentista, id_clinica, avaliacao, comentario) VALUES (3, 2, 2, 5, 'Profissional atencioso e cuidadoso.');
INSERT INTO Feedback (id_cliente, id_dentista, id_clinica, avaliacao, comentario) VALUES (4, 2, 2, 4, 'Ambiente agradável.');
INSERT INTO Feedback (id_cliente, id_dentista, id_clinica, avaliacao, comentario) VALUES (5, 3, 3, 3, 'Esperava mais, mas foi tudo bem.');
INSERT INTO Feedback (id_cliente, id_dentista, id_clinica, avaliacao, comentario) VALUES (6, 3, 3, 4, 'Consulta rápida e eficaz.');
INSERT INTO Feedback (id_cliente, id_dentista, id_clinica, avaliacao, comentario) VALUES (7, 1, 1, 5, 'Recomendo para todos!');
INSERT INTO Feedback (id_cliente, id_dentista, id_clinica, avaliacao, comentario) VALUES (8, 2, 2, 4, 'Foi uma experiência positiva.');
INSERT INTO Feedback (id_cliente, id_dentista, id_clinica, avaliacao, comentario) VALUES (9, 1, 1, 3, 'Um pouco de espera, mas no geral bom.');
INSERT INTO Feedback (id_cliente, id_dentista, id_clinica, avaliacao, comentario) VALUES (10, 3, 3, 5, 'Atendimento de qualidade.');

select * from Feedback;

-- Estado Civil

INSERT INTO Estado_Civil (descricao) VALUES ('Solteiro');
INSERT INTO Estado_Civil (descricao) VALUES ('Casado');
INSERT INTO Estado_Civil (descricao) VALUES ('Divorciado');
INSERT INTO Estado_Civil (descricao) VALUES ('Viúvo');
INSERT INTO Estado_Civil (descricao) VALUES ('União Estável');
INSERT INTO Estado_Civil (descricao) VALUES ('Separado');
INSERT INTO Estado_Civil (descricao) VALUES ('Companheiro');
INSERT INTO Estado_Civil (descricao) VALUES ('Desquitado');
INSERT INTO Estado_Civil (descricao) VALUES ('Noivo');
INSERT INTO Estado_Civil (descricao) VALUES ('Namorando');

select * from Estado_Civil;

-- Formulario Detalhado

INSERT INTO Formulario_Detalhado (id_cliente, id_estado_civil, historico_familiar, profissao, renda_mensal, historico_medico, alergia, condicao_preexistente, uso_medicamento, familiar_com_doencas_dentarias, participacao_em_programas_preventivos, contato_emergencial, pesquisa_satisfacao, data_ultima_atualizacao, frequencia_consulta_periodica, sinalizacao_de_risco, historico_de_viagem, historico_de_mudancas_de_endereco, preferencia_de_contato) VALUES 
(1, 1, 'Nenhum histórico significativo', 'Engenheiro', 5000.00, 'Saudável', 'Nenhuma', 'Hipertensão', 'Apenas paracetamol', 'Não', 'Y', 'Mãe', 'Y', SYSDATE, 'Y', 'Nenhuma', 'Viagens internacionais', 'Mudanças a cada 2 anos', 'Email');

INSERT INTO Formulario_Detalhado (id_cliente, id_estado_civil, historico_familiar, profissao, renda_mensal, historico_medico, alergia, condicao_preexistente, uso_medicamento, familiar_com_doencas_dentarias, participacao_em_programas_preventivos, contato_emergencial, pesquisa_satisfacao, data_ultima_atualizacao, frequencia_consulta_periodica, sinalizacao_de_risco, historico_de_viagem, historico_de_mudancas_de_endereco, preferencia_de_contato) VALUES 
(2, 2, 'Apenas histórico de diabetes na família', 'Médico', 7000.00, 'Saudável', 'Nenhuma', 'Asma', 'Nenhum', 'Sim', 'Y', 'Pai', 'Y', SYSDATE, 'Y', 'Risco baixo', 'Viagens a trabalho', 'Mudança recente', 'Telefone');

INSERT INTO Formulario_Detalhado (id_cliente, id_estado_civil, historico_familiar, profissao, renda_mensal, historico_medico, alergia, condicao_preexistente, uso_medicamento, familiar_com_doencas_dentarias, participacao_em_programas_preventivos, contato_emergencial, pesquisa_satisfacao, data_ultima_atualizacao, frequencia_consulta_periodica, sinalizacao_de_risco, historico_de_viagem, historico_de_mudancas_de_endereco, preferencia_de_contato) VALUES 
(3, 3, 'Histórico de problemas cardíacos', 'Professor', 4000.00, 'Diabetes', 'Alergia a pó', 'Diabetes tipo 2', 'Insulina', 'Sim', 'N', 'Irmã', 'Y', SYSDATE, 'N', 'Risco moderado', 'Sem viagens', 'Mudanças frequentes', 'WhatsApp');

INSERT INTO Formulario_Detalhado (id_cliente, id_estado_civil, historico_familiar, profissao, renda_mensal, historico_medico, alergia, condicao_preexistente, uso_medicamento, familiar_com_doencas_dentarias, participacao_em_programas_preventivos, contato_emergencial, pesquisa_satisfacao, data_ultima_atualizacao, frequencia_consulta_periodica, sinalizacao_de_risco, historico_de_viagem, historico_de_mudancas_de_endereco, preferencia_de_contato) VALUES 
(4, 4, 'Nenhum histórico familiar relevante', 'Advogado', 6000.00, 'Saudável', 'Alergia a amendoim', 'Nenhuma', 'Nenhum', 'Não', 'Y', 'Esposa', 'Y', SYSDATE, 'Y', 'Risco baixo', 'Viagens de férias', 'Mudança a cada 5 anos', 'Email');

INSERT INTO Formulario_Detalhado (id_cliente, id_estado_civil, historico_familiar, profissao, renda_mensal, historico_medico, alergia, condicao_preexistente, uso_medicamento, familiar_com_doencas_dentarias, participacao_em_programas_preventivos, contato_emergencial, pesquisa_satisfacao, data_ultima_atualizacao, frequencia_consulta_periodica, sinalizacao_de_risco, historico_de_viagem, historico_de_mudancas_de_endereco, preferencia_de_contato) VALUES 
(5, 5, 'Histórico de câncer na família', 'Arquiteto', 5500.00, 'Saudável', 'Alergia a poeira', 'Nenhuma', 'Nenhum', 'Sim', 'Y', 'Filho', 'N', SYSDATE, 'Y', 'Risco alto', 'Viagens frequentes', 'Mudança há 1 ano', 'Telefone');

INSERT INTO Formulario_Detalhado (id_cliente, id_estado_civil, historico_familiar, profissao, renda_mensal, historico_medico, alergia, condicao_preexistente, uso_medicamento, familiar_com_doencas_dentarias, participacao_em_programas_preventivos, contato_emergencial, pesquisa_satisfacao, data_ultima_atualizacao, frequencia_consulta_periodica, sinalizacao_de_risco, historico_de_viagem, historico_de_mudancas_de_endereco, preferencia_de_contato) VALUES 
(6, 6, 'Nenhum histórico significativo', 'Dentista', 7500.00, 'Saudável', 'Alergia a medicamentos', 'Nenhuma', 'Nenhum', 'Não', 'Y', 'Amigo', 'Y', SYSDATE, 'N', 'Risco baixo', 'Viagens a trabalho', 'Mudança a cada 3 anos', 'WhatsApp');

INSERT INTO Formulario_Detalhado (id_cliente, id_estado_civil, historico_familiar, profissao, renda_mensal, historico_medico, alergia, condicao_preexistente, uso_medicamento, familiar_com_doencas_dentarias, participacao_em_programas_preventivos, contato_emergencial, pesquisa_satisfacao, data_ultima_atualizacao, frequencia_consulta_periodica, sinalizacao_de_risco, historico_de_viagem, historico_de_mudancas_de_endereco, preferencia_de_contato) VALUES 
(7, 7, 'Histórico de doenças respiratórias', 'Cientista', 4500.00, 'Saudável', 'Alergia a fumaça', 'Nenhuma', 'Nenhum', 'Sim', 'N', 'Cônjuge', 'Y', SYSDATE, 'Y', 'Risco moderado', 'Sem viagens', 'Mudanças frequentes', 'Email');

INSERT INTO Formulario_Detalhado (id_cliente, id_estado_civil, historico_familiar, profissao, renda_mensal, historico_medico, alergia, condicao_preexistente, uso_medicamento, familiar_com_doencas_dentarias, participacao_em_programas_preventivos, contato_emergencial, pesquisa_satisfacao, data_ultima_atualizacao, frequencia_consulta_periodica, sinalizacao_de_risco, historico_de_viagem, historico_de_mudancas_de_endereco, preferencia_de_contato) VALUES 
(8, 8, 'Nenhum histórico familiar relevante', 'Motorista', 3000.00, 'Saudável', 'Alergia a látex', 'Nenhuma', 'Nenhum', 'Não', 'Y', 'Irmão', 'Y', SYSDATE, 'N', 'Risco baixo', 'Viagens ocasionais', 'Mudança a cada 4 anos', 'Telefone');

INSERT INTO Formulario_Detalhado (id_cliente, id_estado_civil, historico_familiar, profissao, renda_mensal, historico_medico, alergia, condicao_preexistente, uso_medicamento, familiar_com_doencas_dentarias, participacao_em_programas_preventivos, contato_emergencial, pesquisa_satisfacao, data_ultima_atualizacao, frequencia_consulta_periodica, sinalizacao_de_risco, historico_de_viagem, historico_de_mudancas_de_endereco, preferencia_de_contato) VALUES 
(9, 9, 'Histórico de hipertensão', 'Estudante', 2000.00, 'Saudável', 'Nenhuma', 'Hipertensão', 'Apenas anti-hipertensivos', 'Sim', 'Y', 'Mãe', 'Y', SYSDATE, 'N', 'Risco baixo', 'Viagens de estudos', 'Mudança a cada 2 anos', 'Email');

select * from Formulario_Detalhado;

-- Tipo de notificação

INSERT INTO Tipo_Notificacao (descricao) VALUES ('Lembrete de Consulta');
INSERT INTO Tipo_Notificacao (descricao) VALUES ('Notificação de Pagamento');
INSERT INTO Tipo_Notificacao (descricao) VALUES ('Confirmação de Agendamento');
INSERT INTO Tipo_Notificacao (descricao) VALUES ('Cancelamento de Consulta');
INSERT INTO Tipo_Notificacao (descricao) VALUES ('Promoção de Serviços');
INSERT INTO Tipo_Notificacao (descricao) VALUES ('Atualização de Cadastro');
INSERT INTO Tipo_Notificacao (descricao) VALUES ('Aviso de Documentação');
INSERT INTO Tipo_Notificacao (descricao) VALUES ('Encaminhamento Médico');
INSERT INTO Tipo_Notificacao (descricao) VALUES ('Pesquisa de Satisfação');
INSERT INTO Tipo_Notificacao (descricao) VALUES ('Alerta de Saúde Preventiva');
select * from Formulario_Detalhado;

select * from Tipo_Notificacao;

-- Agenda

INSERT INTO Agenda (id_cliente, id_consulta, status_consulta, observacoes) 
VALUES (1, 1, 'S', 'Consulta realizada com sucesso.');

INSERT INTO Agenda (id_cliente, id_consulta, status_consulta, observacoes) 
VALUES (2, 2, 'N', 'Consulta cancelada pelo cliente.');

INSERT INTO Agenda (id_cliente, id_consulta, status_consulta, observacoes) 
VALUES (3, 3, 'S', 'Consulta ocorreu conforme planejado.');

INSERT INTO Agenda (id_cliente, id_consulta, status_consulta, observacoes) 
VALUES (4, 4, 'S', 'Cliente compareceu na hora marcada.');

INSERT INTO Agenda (id_cliente, id_consulta, status_consulta, observacoes) 
VALUES (5, 5, 'N', 'Consulta reagendada para a próxima semana.');

INSERT INTO Agenda (id_cliente, id_consulta, status_consulta, observacoes) 
VALUES (6, 6, 'S', 'Procedimento realizado com sucesso.');

INSERT INTO Agenda (id_cliente, id_consulta, status_consulta, observacoes) 
VALUES (7, 7, 'N', 'Consulta não foi confirmada.');

INSERT INTO Agenda (id_cliente, id_consulta, status_consulta, observacoes) 
VALUES (8, 8, 'S', 'Cliente satisfeito com o atendimento.');

INSERT INTO Agenda (id_cliente, id_consulta, status_consulta, observacoes) 
VALUES (9, 9, 'N', 'Consulta não foi realizada.');

INSERT INTO Agenda (id_cliente, id_consulta, status_consulta, observacoes) 
VALUES (10, 10, 'S', 'Consulta finalizada e cliente orientado.');

select * from Agenda;

-- Notificação

INSERT INTO Notificacao (id_cliente, id_tipo_notificacao, mensagem, data_envio) 
VALUES (1, 1, 'Sua consulta está agendada para amanhã.', TO_DATE('2024-10-01', 'YYYY-MM-DD'));

INSERT INTO Notificacao (id_cliente, id_tipo_notificacao, mensagem, data_envio) 
VALUES (2, 2, 'Lembrete: é hora de realizar seu check-up anual.', TO_DATE('2024-09-25', 'YYYY-MM-DD'));

INSERT INTO Notificacao (id_cliente, id_tipo_notificacao, mensagem, data_envio) 
VALUES (3, 1, 'Consulta confirmada para a próxima segunda-feira.', TO_DATE('2024-09-28', 'YYYY-MM-DD'));

INSERT INTO Notificacao (id_cliente, id_tipo_notificacao, mensagem, data_envio) 
VALUES (4, 3, 'Obrigado pelo feedback! Aguardamos sua próxima visita.', TO_DATE('2024-09-30', 'YYYY-MM-DD'));

INSERT INTO Notificacao (id_cliente, id_tipo_notificacao, mensagem, data_envio) 
VALUES (5, 1, 'Sua consulta foi reagendada para a próxima sexta-feira.', TO_DATE('2024-10-03', 'YYYY-MM-DD'));

INSERT INTO Notificacao (id_cliente, id_tipo_notificacao, mensagem, data_envio) 
VALUES (6, 2, 'Faltam 7 dias para seu próximo exame preventivo.', TO_DATE('2024-10-04', 'YYYY-MM-DD'));

INSERT INTO Notificacao (id_cliente, id_tipo_notificacao, mensagem, data_envio) 
VALUES (7, 1, 'Aviso: consulta pendente de confirmação.', TO_DATE('2024-09-27', 'YYYY-MM-DD'));

INSERT INTO Notificacao (id_cliente, id_tipo_notificacao, mensagem, data_envio) 
VALUES (8, 3, 'Estamos prontos para recebê-lo em sua próxima consulta.', TO_DATE('2024-09-29', 'YYYY-MM-DD'));

INSERT INTO Notificacao (id_cliente, id_tipo_notificacao, mensagem, data_envio) 
VALUES (9, 2, 'Não se esqueça de manter suas consultas em dia.', TO_DATE('2024-09-26', 'YYYY-MM-DD'));

INSERT INTO Notificacao (id_cliente, id_tipo_notificacao, mensagem, data_envio) 
VALUES (10, 1, 'Sua consulta foi confirmada para amanhã às 10h.', TO_DATE('2024-09-30', 'YYYY-MM-DD'));

select * from Notificacao;

// Funções de Validação de Entrada de Dados

-- 1. Função para Validar Email
-- Importante para sabermos se o e-mail consta nas regras básicas, com dominio por exemplo, ter o @, e seguir a conversão de .com ou .com.br
/*
Cenários de testes

Email sem @: Deve ser inválido.
Email sem domínio: Deve ser inválido (ex: usuario@).
Email sem nome de usuário: Deve ser inválido (ex: @dominio.com).
Email com caracteres inválidos: Deve ser inválido (ex: usuário!@dominio.com).
Email válido: Deve ser válido (ex: usuario@dominio.com).
Verificar se o email já existe na tabela Cliente.
*/

select * from Cliente;

CREATE OR REPLACE FUNCTION validar_email(p_email IN VARCHAR2) RETURN BOOLEAN IS
    v_count INTEGER;
BEGIN
    -- Verificar se o email contém o símbolo '@'
    IF INSTR(p_email, '@') = 0 THEN
        RETURN FALSE; -- Email inválido, não contém '@'
    END IF;

    -- Verificar se o email tem um domínio após '@'
    IF LENGTH(SUBSTR(p_email, INSTR(p_email, '@') + 1)) = 0 THEN
        RETURN FALSE; -- Email inválido, sem domínio
    END IF;

    -- Verificar se o domínio contém um ponto '.'
    IF INSTR(SUBSTR(p_email, INSTR(p_email, '@') + 1), '.') = 0 THEN
        RETURN FALSE; -- Email inválido, domínio sem ponto
    END IF;

    -- Verificar se o email tem um nome de usuário antes de '@'
    IF LENGTH(SUBSTR(p_email, 1, INSTR(p_email, '@') - 1)) = 0 THEN
        RETURN FALSE; -- Email inválido, sem nome de usuário
    END IF;

    -- Verificar se o email contém caracteres inválidos
    IF REGEXP_LIKE(p_email, '[^a-zA-Z0-9@._-]') THEN
        RETURN FALSE; -- Email inválido, contém caracteres inválidos
    END IF;

    -- Verificar se o email já existe na tabela Cliente
    SELECT COUNT(*)
    INTO v_count
    FROM Cliente
    WHERE email = p_email;

    IF v_count > 0 THEN
        RETURN FALSE; -- Email já existe
    ELSE
        RETURN TRUE; -- Email válido
    END IF;
END;

-- Testar a função com input

CREATE OR REPLACE PROCEDURE solicitar_e_validar_email IS
    v_email VARCHAR2(100);
    v_result BOOLEAN;
BEGIN
    -- Solicitar ao usuário que insira um email
    DBMS_OUTPUT.PUT_LINE('Por favor, insira um email:');
    v_email := '&email'; -- Variável de substituição para entrada do usuário

    -- Chamar a função de validação de email
    v_result := validar_email(v_email);

    -- Exibir o email digitado e o resultado da validação
    DBMS_OUTPUT.PUT_LINE('Email digitado: ' || v_email);
    IF v_result THEN
        DBMS_OUTPUT.PUT_LINE('Resultado: Email válido.');
    ELSE
        -- Verificar se o email já existe na base de dados
        DECLARE
            v_count INTEGER;
        BEGIN
            SELECT COUNT(*)
            INTO v_count
            FROM Cliente
            WHERE email = v_email;

            IF v_count > 0 THEN
                DBMS_OUTPUT.PUT_LINE('Resultado: Email inválido. O email já existe na base de dados.');
            ELSE
                DBMS_OUTPUT.PUT_LINE('Resultado: Email inválido.');
            END IF;
        END;
    END IF;
END;

-- Execute este comando para verificar o resultado
EXEC solicitar_e_validar_email;

-- Procedure para avaliar minha tabela com a validação de e-mail

CREATE OR REPLACE PROCEDURE avaliar_base_de_dados IS
    v_email VARCHAR2(100);
    CURSOR c_emails IS
        SELECT email FROM Cliente;
BEGIN
    -- Verificar se os emails seguem as regras de validação
    DBMS_OUTPUT.PUT_LINE('Verificando formato dos emails...');
    FOR r_email IN c_emails LOOP
        v_email := r_email.email;

        -- Verificar se o email contém o símbolo '@'
        IF INSTR(v_email, '@') = 0 THEN
            DBMS_OUTPUT.PUT_LINE('Erro: Email inválido (sem @): ' || v_email);
        -- Verificar se o email tem um domínio após '@'
        ELSIF LENGTH(SUBSTR(v_email, INSTR(v_email, '@') + 1)) = 0 THEN
            DBMS_OUTPUT.PUT_LINE('Erro: Email inválido (sem domínio): ' || v_email);
        -- Verificar se o domínio contém um ponto '.'
        ELSIF INSTR(SUBSTR(v_email, INSTR(v_email, '@') + 1), '.') = 0 THEN
            DBMS_OUTPUT.PUT_LINE('Erro: Email inválido (domínio sem ponto): ' || v_email);
        -- Verificar se o email tem um nome de usuário antes de '@'
        ELSIF LENGTH(SUBSTR(v_email, 1, INSTR(v_email, '@') - 1)) = 0 THEN
            DBMS_OUTPUT.PUT_LINE('Erro: Email inválido (sem nome de usuário): ' || v_email);
        -- Verificar se o email contém caracteres inválidos
        ELSIF REGEXP_LIKE(v_email, '[^a-zA-Z0-9@._-]') THEN
            DBMS_OUTPUT.PUT_LINE('Erro: Email inválido (caracteres inválidos): ' || v_email);
        ELSE
            DBMS_OUTPUT.PUT_LINE('Email válido: ' || v_email);
        END IF;
    END LOOP;

END;


// Inserts Cliente

INSERT INTO Cliente (nome, sobrenome, email, telefone, data_nasc, endereco) VALUES
('Bruno', 'Santos', 'example.com', '11976543210', TO_DATE('1985-03-20', 'YYYY-MM-DD'), 'Avenida Central, 456');

INSERT INTO Cliente (nome, sobrenome, email, telefone, data_nasc, endereco) VALUES
('Carlos', 'Oliveira', 'carlos.oliveira', '11965432109', TO_DATE('1992-07-11', 'YYYY-MM-DD'), 'Rua dos Lírios, 789');

select * from Cliente;

-- Verifique o resultado
EXEC avaliar_base_de_dados;


-- Função para avaliar Data de Nascimento

CREATE OR REPLACE FUNCTION validar_data_nasc(p_data_nasc IN DATE) RETURN BOOLEAN IS
BEGIN
    -- Verificar se a data de nascimento é no futuro
    IF p_data_nasc > SYSDATE THEN
        RETURN FALSE; -- Data de nascimento no futuro
    END IF;

    -- Verificar se o usuário tem pelo menos 18 anos
    IF MONTHS_BETWEEN(SYSDATE, p_data_nasc) < 18 * 12 THEN
        RETURN FALSE; -- Usuário tem menos de 18 anos
    END IF;

    RETURN TRUE; -- Data de nascimento válida
END;



-- Procedimento para solicitar Data de Nascimento e Validar os dados

CREATE OR REPLACE PROCEDURE solicitar_e_validar_data_nasc IS
    v_data_nasc DATE;
    v_result BOOLEAN;
BEGIN
    -- Solicitar ao usuário que insira uma data de nascimento no formato brasileiro
    DBMS_OUTPUT.PUT_LINE('Por favor, insira uma data de nascimento (DD/MM/YYYY):');
    v_data_nasc := TO_DATE('&data_nasc', 'DD/MM/YYYY'); -- Variável de substituição para entrada do usuário

    -- Chamar a função de validação de data de nascimento
    v_result := validar_data_nasc(v_data_nasc);

    -- Exibir a data de nascimento digitada e o resultado da validação
    DBMS_OUTPUT.PUT_LINE('Data de nascimento digitada: ' || TO_CHAR(v_data_nasc, 'DD/MM/YYYY'));
    IF v_result THEN
        DBMS_OUTPUT.PUT_LINE('Resultado: Data de nascimento válida.');
    ELSE
        DBMS_OUTPUT.PUT_LINE('Resultado: Data de nascimento inválida.');
    END IF;
END;

-- Executar o processo de input
EXEC solicitar_e_validar_data_nasc;


-- Procedimento para avaliar Datas de Nascimento na tabela
CREATE OR REPLACE PROCEDURE avaliar_datas_nasc IS
    v_data_nasc DATE;
    CURSOR c_datas_nasc IS
        SELECT data_nasc FROM Cliente;
BEGIN
    -- Verificar se as datas de nascimento seguem as regras de validação
    DBMS_OUTPUT.PUT_LINE('Verificando datas de nascimento...');
    FOR r_data_nasc IN c_datas_nasc LOOP
        v_data_nasc := r_data_nasc.data_nasc;

        -- Chamar a função de validação de data de nascimento
        IF validar_data_nasc(v_data_nasc) THEN
            DBMS_OUTPUT.PUT_LINE('Data de nascimento válida: ' || TO_CHAR(v_data_nasc, 'DD-MON-YYYY'));
        ELSE
            DBMS_OUTPUT.PUT_LINE('Erro: Data de nascimento inválida: ' || TO_CHAR(v_data_nasc, 'DD-MON-YYYY'));
        END IF;
    END LOOP;

END;


-- Executar o processo na tabela para validar os dados
EXEC avaliar_datas_nasc;


// Procedures para Operações CRUD - Segunda questão

-- Procedure para INSERT na Tabela Cliente

CREATE OR REPLACE PROCEDURE inserir_cliente(
    p_nome IN VARCHAR2,
    p_sobrenome IN VARCHAR2,
    p_email IN VARCHAR2,
    p_telefone IN VARCHAR2,
    p_data_nasc IN DATE,
    p_endereco IN VARCHAR2
) IS
BEGIN
    -- Validar email
    IF NOT validar_email(p_email) THEN
        RAISE_APPLICATION_ERROR(-20001, 'Email inválido ou já existente.');
    END IF;

    -- Validar data de nascimento
    IF NOT validar_data_nasc(p_data_nasc) THEN
        RAISE_APPLICATION_ERROR(-20002, 'Data de nascimento inválida.');
    END IF;

    -- Inserir cliente
    INSERT INTO Cliente (nome, sobrenome, email, telefone, data_nasc, endereco)
    VALUES (p_nome, p_sobrenome, p_email, p_telefone, p_data_nasc, p_endereco);

    DBMS_OUTPUT.PUT_LINE('Cliente inserido com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao inserir cliente: ' || SQLERRM);
END;

-- Testar
EXEC inserir_cliente('Claudio', 'Bispo', 'claudio@example.com', '123456789', TO_DATE('1993-05-10', 'YYYY-MM-DD'), 'Rua Aurea da Gama');

select * from Cliente;

-- Procedure para UPDATE na Tabela Cliente

CREATE OR REPLACE PROCEDURE atualizar_cliente(
    p_id_cliente IN INTEGER,
    p_nome IN VARCHAR2,
    p_sobrenome IN VARCHAR2,
    p_email IN VARCHAR2,
    p_telefone IN VARCHAR2,
    p_data_nasc IN DATE,
    p_endereco IN VARCHAR2
) IS
BEGIN
    -- Validar email
    IF NOT validar_email(p_email) THEN
        RAISE_APPLICATION_ERROR(-20001, 'Email inválido ou já existente.');
    END IF;

    -- Validar data de nascimento
    IF NOT validar_data_nasc(p_data_nasc) THEN
        RAISE_APPLICATION_ERROR(-20002, 'Data de nascimento inválida.');
    END IF;

    -- Atualizar cliente
    UPDATE Cliente
    SET nome = p_nome,
        sobrenome = p_sobrenome,
        email = p_email,
        telefone = p_telefone,
        data_nasc = p_data_nasc,
        endereco = p_endereco
    WHERE id_cliente = p_id_cliente;

    DBMS_OUTPUT.PUT_LINE('Cliente atualizado com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao atualizar cliente: ' || SQLERRM);
END;

-- Testar o Update

select * from Cliente;

EXEC atualizar_cliente(1, 'Nome Atualizado', 'Sobrenome Atualizado', 'email_atualizado@example.com', '987654321', TO_DATE('01/01/1990', 'DD/MM/YYYY'), 'Endereço Atualizado');

select * from Cliente;

-- Procedure para DELETE na Tabela Cliente

CREATE OR REPLACE PROCEDURE deletar_cliente(
    p_id_cliente IN INTEGER
) IS
BEGIN
    -- Deletar cliente
    DELETE FROM Cliente
    WHERE id_cliente = p_id_cliente;

    DBMS_OUTPUT.PUT_LINE('Cliente deletado com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao deletar cliente: ' || SQLERRM);
END;

-- Testar o Delete

EXEC deletar_cliente(14);
select * from Cliente;

-- Procedure para INSERT na Tabela Clinica

CREATE OR REPLACE PROCEDURE inserir_clinica(
    p_nome IN VARCHAR2,
    p_endereco IN VARCHAR2,
    p_telefone IN VARCHAR2,
    p_avaliacao IN NUMBER,
    p_preco_medio IN NUMBER
) IS
BEGIN
    -- Inserir clínica
    INSERT INTO Clinica (nome, endereco, telefone, avaliacao, preco_medio)
    VALUES (p_nome, p_endereco, p_telefone, p_avaliacao, p_preco_medio);

    DBMS_OUTPUT.PUT_LINE('Clínica inserida com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao inserir clínica: ' || SQLERRM);
END;


-- Procedure para UPDATE na Tabela Clinica

CREATE OR REPLACE PROCEDURE atualizar_clinica(
    p_id_clinica IN INTEGER,
    p_nome IN VARCHAR2,
    p_endereco IN VARCHAR2,
    p_telefone IN VARCHAR2,
    p_avaliacao IN DECIMAL,
    p_preco_medio IN DECIMAL
) IS
BEGIN
    -- Atualizar clínica
    UPDATE Clinica
    SET nome = p_nome,
        endereco = p_endereco,
        telefone = p_telefone,
        avaliacao = p_avaliacao,
        preco_medio = p_preco_medio
    WHERE id_clinica = p_id_clinica;

    DBMS_OUTPUT.PUT_LINE('Clínica atualizada com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao atualizar clínica: ' || SQLERRM);
END;

-- Procedure para DELETE na Tabela Clinica

CREATE OR REPLACE PROCEDURE deletar_clinica(
    p_id_clinica IN INTEGER
) IS
BEGIN
    -- Deletar clínica
    DELETE FROM Clinica
    WHERE id_clinica = p_id_clinica;

    DBMS_OUTPUT.PUT_LINE('Clínica deletada com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao deletar clínica: ' || SQLERRM);
END;

-- Teste para os próximos, vou conferir tudo de uma vez, visto que em Cliente funcionou.
-- Testar os três processos

select * from Clinica;
EXEC inserir_clinica('Clínica Saude e Sorriso', 'Rua das Flores, 123', '11987654321', 4, 150);
EXEC atualizar_clinica (1, 'Nome Atualizado da Clinica', 'Endereço Atualizado', '987654321', 5, 200);
EXEC deletar_clinica(11);

-- Procedure para INSERT na Tabela Especialidade
select * from especialidade;

CREATE OR REPLACE PROCEDURE inserir_especialidade(
    p_nome IN VARCHAR2
) IS
BEGIN
    -- Inserir especialidade
    INSERT INTO Especialidade (nome)
    VALUES (p_nome);

    DBMS_OUTPUT.PUT_LINE('Especialidade inserida com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao inserir especialidade: ' || SQLERRM);
END;



-- Procedure para UPDATE na Tabela Especialidade

CREATE OR REPLACE PROCEDURE atualizar_especialidade(
    p_id_especialidade IN INTEGER,
    p_nome IN VARCHAR2
) IS
BEGIN
    -- Atualizar especialidade
    UPDATE Especialidade
    SET nome = p_nome
    WHERE id_especialidade = p_id_especialidade;

    DBMS_OUTPUT.PUT_LINE('Especialidade atualizada com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao atualizar especialidade: ' || SQLERRM);
END;



-- Procedure para DELETE na Tabela Especialidade

CREATE OR REPLACE PROCEDURE deletar_especialidade(
    p_id_especialidade IN INTEGER
) IS
BEGIN
    -- Deletar especialidade
    DELETE FROM Especialidade
    WHERE id_especialidade = p_id_especialidade;

    DBMS_OUTPUT.PUT_LINE('Especialidade deletada com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao deletar especialidade: ' || SQLERRM);
END;



-- Teste
select * from especialidade;
EXEC inserir_especialidade('Ortodontia');
EXEC atualizar_especialidade(1, 'Endodontia');
EXEC deletar_especialidade(11);

-- Procedure para INSERT na Tabela Dentista

select * from dentista;

CREATE OR REPLACE PROCEDURE inserir_dentista(
    p_nome IN VARCHAR2,
    p_sobrenome IN VARCHAR2,
    p_telefone IN VARCHAR2,
    p_id_clinica IN INTEGER,
    p_id_especialidade IN INTEGER,
    p_avaliacao IN NUMBER
) IS
BEGIN
    -- Inserir dentista
    INSERT INTO Dentista (nome, sobrenome, telefone, id_clinica, id_especialidade, avaliacao)
    VALUES (p_nome, p_sobrenome, p_telefone, p_id_clinica, p_id_especialidade, p_avaliacao);

    DBMS_OUTPUT.PUT_LINE('Dentista inserido com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao inserir dentista: ' || SQLERRM);
END;



-- Procedure para UPDATE na Tabela Dentista

CREATE OR REPLACE PROCEDURE atualizar_dentista(
    p_id_dentista IN INTEGER,
    p_nome IN VARCHAR2,
    p_sobrenome IN VARCHAR2,
    p_telefone IN VARCHAR2,
    p_id_clinica IN INTEGER,
    p_id_especialidade IN INTEGER,
    p_avaliacao IN NUMBER
) IS
BEGIN
    -- Atualizar dentista
    UPDATE Dentista
    SET nome = p_nome,
        sobrenome = p_sobrenome,
        telefone = p_telefone,
        id_clinica = p_id_clinica,
        id_especialidade = p_id_especialidade,
        avaliacao = p_avaliacao
    WHERE id_dentista = p_id_dentista;

    DBMS_OUTPUT.PUT_LINE('Dentista atualizado com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao atualizar dentista: ' || SQLERRM);
END;



-- Procedure para DELETE na Tabela Dentista

CREATE OR REPLACE PROCEDURE deletar_dentista(
    p_id_dentista IN INTEGER
) IS
BEGIN
    -- Deletar dentista
    DELETE FROM Dentista
    WHERE id_dentista = p_id_dentista;

    DBMS_OUTPUT.PUT_LINE('Dentista deletado com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao deletar dentista: ' || SQLERRM);
END;

-- Teste
select * from dentista;

-- Inserir dados na  tabela para achar a chave mãe, uma vez que não deixamos dados dentro das tabelas para realizar os testes das procedures
INSERT INTO Especialidade (nome) VALUES ('Dentística');
select * from especialidade;

INSERT INTO Clinica (nome, endereco, telefone, avaliacao, preco_medio) VALUES ('Clinica Saude e Sorriso', 'Rua das Flores, 123', '11987654321', 4, 150);
select * from Clinica;

EXEC inserir_dentista('João', 'Silva', '11987654321', 2, 2, 4);
EXEC atualizar_dentista(1, 'João', 'Silva', '11987654321', 1, 1, 5);

select * from dentista;
EXEC deletar_dentista(11); -- ajustar para o id que consta no select antes de executar. Defina o que deseja excluir.


-- Procedure para INSERT na Tabela Feedback

CREATE OR REPLACE PROCEDURE inserir_feedback(
    p_id_cliente IN INTEGER,
    p_id_dentista IN INTEGER,
    p_id_clinica IN INTEGER,
    p_avaliacao IN NUMBER,
    p_comentario IN VARCHAR2
) IS
BEGIN
    -- Inserir feedback
    INSERT INTO Feedback (id_cliente, id_dentista, id_clinica, avaliacao, comentario)
    VALUES (p_id_cliente, p_id_dentista, p_id_clinica, p_avaliacao, p_comentario);

    DBMS_OUTPUT.PUT_LINE('Feedback inserido com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao inserir feedback: ' || SQLERRM);
END;


-- Procedure para UPDATE na Tabela Feedback

CREATE OR REPLACE PROCEDURE atualizar_feedback(
    p_id_feedback IN INTEGER,
    p_id_cliente IN INTEGER,
    p_id_dentista IN INTEGER,
    p_id_clinica IN INTEGER,
    p_avaliacao IN NUMBER,
    p_comentario IN VARCHAR2
) IS
BEGIN
    -- Atualizar feedback
    UPDATE Feedback
    SET id_cliente = p_id_cliente,
        id_dentista = p_id_dentista,
        id_clinica = p_id_clinica,
        avaliacao = p_avaliacao,
        comentario = p_comentario
    WHERE id_feedback = p_id_feedback;

    DBMS_OUTPUT.PUT_LINE('Feedback atualizado com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao atualizar feedback: ' || SQLERRM);
END;



-- Procedure para DELETE na Tabela Feedback

CREATE OR REPLACE PROCEDURE deletar_feedback(
    p_id_feedback IN INTEGER
) IS
BEGIN
    -- Deletar feedback
    DELETE FROM Feedback
    WHERE id_feedback = p_id_feedback;

    DBMS_OUTPUT.PUT_LINE('Feedback deletado com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao deletar feedback: ' || SQLERRM);
END;


-- Teste Feedback

select * from Feedback;

EXEC inserir_feedback(1, 1, 1, 4, 'Otimo atendimento!');
EXEC atualizar_feedback(11, 1, 1, 1, 5, 'Atendimento excelente!');
EXEC deletar_feedback(11);


-- Procedure para INSERT na Tabela Consulta

select * from Consulta;

CREATE OR REPLACE PROCEDURE inserir_consulta(
    p_id_cliente IN INTEGER,
    p_id_clinica IN INTEGER,
    p_id_dentista IN INTEGER,
    p_tipo_servico IN VARCHAR2,
    p_data_consulta IN TIMESTAMP,
    p_status_consulta IN CHAR,
    p_observacoes IN VARCHAR2,
    p_sintomas IN VARCHAR2,
    p_tratamento_recomendado IN VARCHAR2,
    p_custo IN NUMBER,
    p_prescricao IN VARCHAR2,
    p_data_retorno IN DATE
) IS
BEGIN
    -- Inserir consulta
    INSERT INTO Consulta (id_cliente, id_clinica, id_dentista, tipo_servico, data_consulta, status_consulta, observacoes, sintomas, tratamento_recomendado, custo, prescricao, data_retorno)
    VALUES (p_id_cliente, p_id_clinica, p_id_dentista, p_tipo_servico, p_data_consulta, p_status_consulta, p_observacoes, p_sintomas, p_tratamento_recomendado, p_custo, p_prescricao, p_data_retorno);

    DBMS_OUTPUT.PUT_LINE('Consulta inserida com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao inserir consulta: ' || SQLERRM);
END;

-- Procedure para UPDATE na Tabela Consulta

CREATE OR REPLACE PROCEDURE atualizar_consulta(
    p_id_consulta IN INTEGER,
    p_id_cliente IN INTEGER,
    p_id_clinica IN INTEGER,
    p_id_dentista IN INTEGER,
    p_tipo_servico IN VARCHAR2,
    p_data_consulta IN TIMESTAMP,
    p_status_consulta IN CHAR,
    p_observacoes IN VARCHAR2,
    p_sintomas IN VARCHAR2,
    p_tratamento_recomendado IN VARCHAR2,
    p_custo IN NUMBER,
    p_prescricao IN VARCHAR2,
    p_data_retorno IN DATE
) IS
BEGIN
    -- Atualizar consulta
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
    WHERE id_consulta = p_id_consulta;

    DBMS_OUTPUT.PUT_LINE('Consulta atualizada com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao atualizar consulta: ' || SQLERRM);
END;

-- Procedure para DELETE na Tabela Consulta

CREATE OR REPLACE PROCEDURE deletar_consulta(
    p_id_consulta IN INTEGER
) IS
BEGIN
    -- Deletar consulta
    DELETE FROM Consulta
    WHERE id_consulta = p_id_consulta;

    DBMS_OUTPUT.PUT_LINE('Consulta deletada com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao deletar consulta: ' || SQLERRM);
END;

-- Testes

select * from Consulta;

EXEC inserir_consulta(1, 1, 1, 'Limpeza', TO_TIMESTAMP('2024-10-19 10:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'S', 'Nenhuma', 'Nenhum', 'Limpeza básica', 100, 'Nenhuma', TO_DATE('2024-11-19', 'YYYY-MM-DD'));
EXEC atualizar_consulta(11, 1, 1, 1, 'Tirar o dente', TO_TIMESTAMP('2024-10-19 10:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'S', 'Nenhuma', 'Nenhum', 'Limpeza básica', 200, 'Nenhuma', TO_DATE('2024-11-19', 'YYYY-MM-DD'));
EXEC deletar_consulta(11);


-- Procedure para INSERT na Tabela Sinistro

CREATE OR REPLACE PROCEDURE inserir_sinistro(
    p_id_consulta IN INTEGER,
    p_nome IN VARCHAR2,
    p_descricao IN VARCHAR2,
    p_status_sinistro IN CHAR,
    p_descricao_status IN VARCHAR2,
    p_valor_sinistro IN NUMBER,
    p_data_abertura IN DATE,
    p_data_resolucao IN DATE,
    p_documentacao IN VARCHAR2
) IS
BEGIN
    -- Inserir sinistro
    INSERT INTO Sinistro (id_consulta, nome, descricao, status_sinistro, descricao_status, valor_sinistro, data_abertura, data_resolucao, documentacao)
    VALUES (p_id_consulta, p_nome, p_descricao, p_status_sinistro, p_descricao_status, p_valor_sinistro, p_data_abertura, p_data_resolucao, p_documentacao);

    DBMS_OUTPUT.PUT_LINE('Sinistro inserido com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao inserir sinistro: ' || SQLERRM);
END;

-- Procedure para UPDATE na Tabela Sinistro

CREATE OR REPLACE PROCEDURE atualizar_sinistro(
    p_id_sinistro IN INTEGER,
    p_id_consulta IN INTEGER,
    p_nome IN VARCHAR2,
    p_descricao IN VARCHAR2,
    p_status_sinistro IN CHAR,
    p_descricao_status IN VARCHAR2,
    p_valor_sinistro IN NUMBER,
    p_data_abertura IN DATE,
    p_data_resolucao IN DATE,
    p_documentacao IN VARCHAR2
) IS
BEGIN
    -- Atualizar sinistro
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
    WHERE id_sinistro = p_id_sinistro;

    DBMS_OUTPUT.PUT_LINE('Sinistro atualizado com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao atualizar sinistro: ' || SQLERRM);
END;

-- Procedure para DELETE na Tabela Sinistro

CREATE OR REPLACE PROCEDURE deletar_sinistro(
    p_id_sinistro IN INTEGER
) IS
BEGIN
    -- Deletar sinistro
    DELETE FROM Sinistro
    WHERE id_sinistro = p_id_sinistro;

    DBMS_OUTPUT.PUT_LINE('Sinistro deletado com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao deletar sinistro: ' || SQLERRM);
END;

-- Teste Sinistro

select * from Sinistro;

EXEC inserir_sinistro(1, 'Sinistro Exemplo', 'Descrição do sinistro', 'S', 'Status do sinistro', 500, TO_DATE('2024-10-19', 'YYYY-MM-DD'), TO_DATE('2024-11-19', 'YYYY-MM-DD'), 'Documentação');
EXEC atualizar_sinistro(11, 1, 'Sinistro Atualizado', 'Descrição atualizada', 'N', 'Status atualizado', 600, TO_DATE('2024-10-19', 'YYYY-MM-DD'), TO_DATE('2024-11-19', 'YYYY-MM-DD'), 'Documentação atualizada');
EXEC deletar_sinistro(11);


-- Procedure para INSERT na Tabela Estado_Civil

select * from Estado_Civil;

CREATE OR REPLACE PROCEDURE inserir_estado_civil(
    p_descricao IN VARCHAR2
) IS
BEGIN
    -- Inserir estado civil
    INSERT INTO Estado_Civil (descricao)
    VALUES (p_descricao);

    DBMS_OUTPUT.PUT_LINE('Estado civil inserido com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao inserir estado civil: ' || SQLERRM);
END;

-- Procedure para UPDATE na Tabela Estado_Civil

CREATE OR REPLACE PROCEDURE atualizar_estado_civil(
    p_id_estado_civil IN INTEGER,
    p_descricao IN VARCHAR2
) IS
BEGIN
    -- Atualizar estado civil
    UPDATE Estado_Civil
    SET descricao = p_descricao
    WHERE id_estado_civil = p_id_estado_civil;

    DBMS_OUTPUT.PUT_LINE('Estado civil atualizado com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao atualizar estado civil: ' || SQLERRM);
END;

-- Procedure para DELETE na Tabela Estado_Civil

CREATE OR REPLACE PROCEDURE deletar_estado_civil(
    p_id_estado_civil IN INTEGER
) IS
BEGIN
    -- Deletar estado civil
    DELETE FROM Estado_Civil
    WHERE id_estado_civil = p_id_estado_civil;

    DBMS_OUTPUT.PUT_LINE('Estado civil deletado com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao deletar estado civil: ' || SQLERRM);
END;



-- Testes

select * from Estado_Civil;

EXEC inserir_estado_civil('Novo');
EXEC atualizar_estado_civil(11, 'Enrolado');
EXEC deletar_estado_civil(11);


-- Procedure para INSERT na Tabela Formulario_Detalhado

CREATE OR REPLACE PROCEDURE inserir_formulario_detalhado(
    p_id_cliente IN INTEGER,
    p_id_estado_civil IN INTEGER,
    p_historico_familiar IN VARCHAR2,
    p_profissao IN VARCHAR2,
    p_renda_mensal IN NUMBER,
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
    -- Inserir formulário detalhado
    INSERT INTO Formulario_Detalhado (id_cliente, id_estado_civil, historico_familiar, profissao, renda_mensal, historico_medico, alergia, condicao_preexistente, uso_medicamento, familiar_com_doencas_dentarias, participacao_em_programas_preventivos, contato_emergencial, pesquisa_satisfacao, data_ultima_atualizacao, frequencia_consulta_periodica, sinalizacao_de_risco, historico_de_viagem, historico_de_mudancas_de_endereco, preferencia_de_contato)
    VALUES (p_id_cliente, p_id_estado_civil, p_historico_familiar, p_profissao, p_renda_mensal, p_historico_medico, p_alergia, p_condicao_preexistente, p_uso_medicamento, p_familiar_com_doencas_dentarias, p_participacao_em_programas_preventivos, p_contato_emergencial, p_pesquisa_satisfacao, p_data_ultima_atualizacao, p_frequencia_consulta_periodica, p_sinalizacao_de_risco, p_historico_de_viagem, p_historico_de_mudancas_de_endereco, p_preferencia_de_contato);

    DBMS_OUTPUT.PUT_LINE('Formulário detalhado inserido com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao inserir formulário detalhado: ' || SQLERRM);
END;

-- Procedure para UPDATE na Tabela Formulario_Detalhado

CREATE OR REPLACE PROCEDURE atualizar_formulario_detalhado(
    p_id_formulario IN INTEGER,
    p_id_cliente IN INTEGER,
    p_id_estado_civil IN INTEGER,
    p_historico_familiar IN VARCHAR2,
    p_profissao IN VARCHAR2,
    p_renda_mensal IN NUMBER,
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
    -- Atualizar formulário detalhado
    UPDATE Formulario_Detalhado
    SET id_cliente = p_id_cliente,
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

    DBMS_OUTPUT.PUT_LINE('Formulário detalhado atualizado com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao atualizar formulário detalhado: ' || SQLERRM);
END;

-- Procedure para DELETE na Tabela Formulario_Detalhado

CREATE OR REPLACE PROCEDURE deletar_formulario_detalhado(
    p_id_formulario IN INTEGER
) IS
BEGIN
    -- Deletar formulário detalhado
    DELETE FROM Formulario_Detalhado
    WHERE id_formulario = p_id_formulario;

    DBMS_OUTPUT.PUT_LINE('Formulário detalhado deletado com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao deletar formulário detalhado: ' || SQLERRM);
END;

-- Teste

select * from Formulario_Detalhado;

EXEC inserir_formulario_detalhado(1, 1, 'Histórico Familiar', 'Profissão', 5000, 'Histórico Médico', 'Alergia', 'Condição Preexistente', 'Uso de Medicamento', 'Familiar com Doenças Dentárias', 'Y', 'Contato Emergencial', 'Y', TO_DATE('2024-10-19', 'YYYY-MM-DD'), 'Y', 'Sinalização de Risco', 'Histórico de Viagem', 'Histórico de Mudanças de Endereço', 'Preferência de Contato');
EXEC atualizar_formulario_detalhado(10, 1, 1, 'Histórico Familiar Atualizado', 'Profissão Atualizada', 6000, 'Histórico Médico Atualizado', 'Alergia Atualizada', 'Condição Preexistente Atualizada', 'Uso de Medicamento Atualizado', 'Familiar com Doenças Dentárias Atualizado', 'N', 'Contato Emergencial Atualizado', 'N', TO_DATE('2024-10-20', 'YYYY-MM-DD'), 'N', 'Sinalização de Risco Atualizada', 'Histórico de Viagem Atualizado', 'Histórico de Mudanças de Endereço Atualizado', 'Preferência de Contato Atualizada');
EXEC deletar_formulario_detalhado(1);

-- Procedure para INSERT na Tabela Tipo_Notificacao

CREATE OR REPLACE PROCEDURE inserir_tipo_notificacao(
    p_descricao IN VARCHAR2
) IS
BEGIN
    -- Inserir tipo de notificação
    INSERT INTO Tipo_Notificacao (descricao)
    VALUES (p_descricao);

    DBMS_OUTPUT.PUT_LINE('Tipo de notificação inserido com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao inserir tipo de notificação: ' || SQLERRM);
END;

-- Procedure para UPDATE na Tabela Tipo_Notificacao

CREATE OR REPLACE PROCEDURE atualizar_tipo_notificacao(
    p_id_tipo_notificacao IN INTEGER,
    p_descricao IN VARCHAR2
) IS
BEGIN
    -- Atualizar tipo de notificação
    UPDATE Tipo_Notificacao
    SET descricao = p_descricao
    WHERE id_tipo_notificacao = p_id_tipo_notificacao;

    DBMS_OUTPUT.PUT_LINE('Tipo de notificação atualizado com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao atualizar tipo de notificação: ' || SQLERRM);
END;

-- Procedure para DELETE na Tabela Tipo_Notificacao

CREATE OR REPLACE PROCEDURE deletar_tipo_notificacao(
    p_id_tipo_notificacao IN INTEGER
) IS
BEGIN
    -- Deletar tipo de notificação
    DELETE FROM Tipo_Notificacao
    WHERE id_tipo_notificacao = p_id_tipo_notificacao;

    DBMS_OUTPUT.PUT_LINE('Tipo de notificação deletado com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao deletar tipo de notificação: ' || SQLERRM);
END;

-- Testes

select * from Tipo_Notificacao;

EXEC inserir_tipo_notificacao('Aviso');
EXEC atualizar_tipo_notificacao(11, 'Alerta');
EXEC deletar_tipo_notificacao(11);

-- Procedure para INSERT na Tabela Agenda

CREATE OR REPLACE PROCEDURE inserir_agenda(
    p_id_cliente IN INTEGER,
    p_id_consulta IN INTEGER,
    p_status_consulta IN CHAR,
    p_observacoes IN VARCHAR2
) IS
BEGIN
    -- Inserir agenda
    INSERT INTO Agenda (id_cliente, id_consulta, status_consulta, observacoes)
    VALUES (p_id_cliente, p_id_consulta, p_status_consulta, p_observacoes);

    DBMS_OUTPUT.PUT_LINE('Agenda inserida com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao inserir agenda: ' || SQLERRM);
END;

-- Procedure para UPDATE na Tabela Agenda

CREATE OR REPLACE PROCEDURE atualizar_agenda(
    p_id_agenda IN INTEGER,
    p_id_cliente IN INTEGER,
    p_id_consulta IN INTEGER,
    p_status_consulta IN CHAR,
    p_observacoes IN VARCHAR2
) IS
BEGIN
    -- Atualizar agenda
    UPDATE Agenda
    SET id_cliente = p_id_cliente,
        id_consulta = p_id_consulta,
        status_consulta = p_status_consulta,
        observacoes = p_observacoes
    WHERE id_agenda = p_id_agenda;

    DBMS_OUTPUT.PUT_LINE('Agenda atualizada com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao atualizar agenda: ' || SQLERRM);
END;


-- Procedure para DELETE na Tabela Agenda

CREATE OR REPLACE PROCEDURE deletar_agenda(
    p_id_agenda IN INTEGER
) IS
BEGIN
    -- Deletar agenda
    DELETE FROM Agenda
    WHERE id_agenda = p_id_agenda;

    DBMS_OUTPUT.PUT_LINE('Agenda deletada com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao deletar agenda: ' || SQLERRM);
END;

-- Testes

select * from Agenda;

EXEC inserir_agenda(1, 1, 'S', 'Consulta de rotina');
EXEC atualizar_agenda(11, 1, 1, 'N', 'Consulta cancelada');
EXEC deletar_agenda(11);


-- Procedure para INSERT na Tabela Notificacao

CREATE OR REPLACE PROCEDURE inserir_notificacao(
    p_id_cliente IN INTEGER,
    p_id_tipo_notificacao IN INTEGER,
    p_mensagem IN VARCHAR2,
    p_data_envio IN DATE
) IS
BEGIN
    -- Inserir notificação
    INSERT INTO Notificacao (id_cliente, id_tipo_notificacao, mensagem, data_envio)
    VALUES (p_id_cliente, p_id_tipo_notificacao, p_mensagem, p_data_envio);

    DBMS_OUTPUT.PUT_LINE('Notificação inserida com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao inserir notificação: ' || SQLERRM);
END;

-- Procedure para UPDATE na Tabela Notificacao

CREATE OR REPLACE PROCEDURE atualizar_notificacao(
    p_id_notificacao IN INTEGER,
    p_id_cliente IN INTEGER,
    p_id_tipo_notificacao IN INTEGER,
    p_mensagem IN VARCHAR2,
    p_data_envio IN DATE
) IS
BEGIN
    -- Atualizar notificação
    UPDATE Notificacao
    SET id_cliente = p_id_cliente,
        id_tipo_notificacao = p_id_tipo_notificacao,
        mensagem = p_mensagem,
        data_envio = p_data_envio
    WHERE id_notificacao = p_id_notificacao;

    DBMS_OUTPUT.PUT_LINE('Notificação atualizada com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao atualizar notificação: ' || SQLERRM);
END;

-- Procedure para DELETE na Tabela Notificacao

CREATE OR REPLACE PROCEDURE deletar_notificacao(
    p_id_notificacao IN INTEGER
) IS
BEGIN
    -- Deletar notificação
    DELETE FROM Notificacao
    WHERE id_notificacao = p_id_notificacao;

    DBMS_OUTPUT.PUT_LINE('Notificação deletada com sucesso.');
EXCEPTION
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Erro ao deletar notificação: ' || SQLERRM);
END;

-- Testes

select * from Notificacao;

EXEC inserir_notificacao(1, 1, 'Sua consulta está agendada para amanhã.', TO_DATE('2024-10-19', 'YYYY-MM-DD'));
EXEC atualizar_notificacao(11, 1, 1, 'Sua consulta foi reagendada.', TO_DATE('2024-10-20', 'YYYY-MM-DD'));
EXEC deletar_notificacao(11);


// Função com Cursor e Joins para Relatório Formatado

-- Função para os relatório de Consultas -- Neste processo, tudo foi finalizado já.
-- Relatório com informações de consultas, incluindo detalhes do cliente, dentista e clínica.
-- JOINs entre as tabelas Consulta, Cliente, Dentista e Clinica.

CREATE OR REPLACE FUNCTION relatorio_consultas RETURN SYS_REFCURSOR IS
    v_cursor SYS_REFCURSOR;
BEGIN
    OPEN v_cursor FOR
        SELECT c.id_consulta,
               cl.nome AS nome_cliente,
               cl.sobrenome AS sobrenome_cliente,
               d.nome AS nome_dentista,
               d.sobrenome AS sobrenome_dentista,
               clc.nome AS nome_clinica,
               c.tipo_servico,
               c.data_consulta,
               c.status_consulta,
               c.observacoes,
               c.sintomas,
               c.tratamento_recomendado,
               c.custo,
               c.prescricao,
               c.data_retorno
        FROM Consulta c
        JOIN Cliente cl ON c.id_cliente = cl.id_cliente
        JOIN Dentista d ON c.id_dentista = d.id_dentista
        JOIN Clinica clc ON c.id_clinica = clc.id_clinica;
    RETURN v_cursor;
END;

-- Teste
-- Gera um relatório formatado com informações detalhadas sobre as consultas.
VARIABLE rc REFCURSOR;
EXEC :rc := relatorio_consultas;
PRINT rc;

// Função para relatório com regra de negócio

-- Vamos criar uma função que gera um relatório com a contagem de consultas por clínica, incluindo a soma dos custos das consultas. 
-- Isso pode ajudar a entender a carga de trabalho e a receita gerada por cada clínica.

CREATE OR REPLACE FUNCTION relatorio_consultas_por_clinica RETURN SYS_REFCURSOR IS
    v_cursor SYS_REFCURSOR;
BEGIN
    OPEN v_cursor FOR
        SELECT clc.nome AS nome_clinica,
               COUNT(c.id_consulta) AS total_consultas,
               SUM(c.custo) AS receita_total
        FROM Consulta c
        JOIN Clinica clc ON c.id_clinica = clc.id_clinica
        GROUP BY clc.nome
        ORDER BY total_consultas DESC;
    RETURN v_cursor;
END;

-- Teste

VARIABLE rc REFCURSOR;
EXEC :rc := relatorio_consultas_por_clinica;
PRINT rc;
