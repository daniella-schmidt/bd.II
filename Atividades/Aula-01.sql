-- BANCO DE DADOS: NOTA FISCAL
CREATE DATABASE NOTA_FISCAL_NORMALIZADA;
GO

USE NOTA_FISCAL_NORMALIZADA;
GO

-- Tabela NOTA_FISCAL
CREATE TABLE NOTA_FISCAL(
    NRO_NOTA INT IDENTITY(1,1) PRIMARY KEY,
    NM_CLIENTE VARCHAR(256) NOT NULL,
    END_CLIENTE VARCHAR(256) NOT NULL,
    NM_VENDEDOR VARCHAR(256) NOT NULL,
    DT_EMISSAO DATETIME DEFAULT GETDATE(),
    VL_TOTAL FLOAT NOT NULL
);
GO

-- Tabela PRODUTO
CREATE TABLE PRODUTO(
    COD_PRODUTO INT IDENTITY(1,1) PRIMARY KEY,
    DESC_PRODUTO VARCHAR(256) NOT NULL,
    UN_MED CHAR(2) NOT NULL,
    VL_PRODUTO FLOAT NOT NULL
);
GO

-- Tabela ITEM_NOTA_FISCAL
CREATE TABLE ITEM_NOTA_FISCAL(
    NRO_NOTA INT NOT NULL,
    COD_PRODUTO INT NOT NULL,
    QTD_PRODUTO INT NOT NULL,
    VL_PRECO FLOAT NOT NULL,
    VL_TOTAL FLOAT NOT NULL,
    CONSTRAINT PK_ITEM_NOTA PRIMARY KEY (NRO_NOTA, COD_PRODUTO),
    CONSTRAINT FK_NRO_NOTA_FISCAL FOREIGN KEY (NRO_NOTA) REFERENCES NOTA_FISCAL (NRO_NOTA),
    CONSTRAINT FK_COD_PRODUTO FOREIGN KEY (COD_PRODUTO) REFERENCES PRODUTO (COD_PRODUTO)
);
GO

-- Inserindo Produtos
INSERT INTO PRODUTO (DESC_PRODUTO, UN_MED, VL_PRODUTO)
VALUES ('LEITE', 'LT', 4.50),
       ('DESODORANTE', 'UN', 8),
       ('SALAME', 'KG', 40),
       ('BOLA', 'KG', 1);
GO

-- Inserindo Notas Fiscais
INSERT INTO NOTA_FISCAL (NM_CLIENTE, END_CLIENTE, NM_VENDEDOR, VL_TOTAL)
VALUES ('CHRISTOPHER', 'SIDNEY', 'FELIX', 4500),
       ('JENO', 'SEOUL', 'JAEMIN', 1500),
       ('MARK', 'TORONTO', 'FELIX', 4500);
GO

-- Inserindo Itens da Nota Fiscal
INSERT INTO ITEM_NOTA_FISCAL (NRO_NOTA, COD_PRODUTO, QTD_PRODUTO, VL_PRECO, VL_TOTAL)
VALUES (1, 2, 2, 55, 65),
       (3, 2, 1, 85, 45),
       (3, 1, 3, 85.4, 74.5),
       (2, 1, 2, 51.2, 43.5),
       (2, 2, 2, 73, 27),
       (2, 3, 2, 83, 149);
GO

-- Consultar Produtos
SELECT * FROM PRODUTO;
GO

-- Atualizar Produto
UPDATE PRODUTO 
SET VL_PRODUTO = 50, DESC_PRODUTO = 'ATUALIZADO', UN_MED = 'CX'
WHERE COD_PRODUTO = 3;
GO

-- Tentar Excluir Produto com dependência (irá falhar devido à FK)
DELETE FROM PRODUTO WHERE COD_PRODUTO = 3;
GO

-- Inserir e Excluir Produto sem dependência
INSERT INTO PRODUTO (DESC_PRODUTO, UN_MED, VL_PRODUTO)
VALUES ('TESTES DELETE', 'LT', 127);
GO

DELETE FROM PRODUTO WHERE COD_PRODUTO = 5;
GO

-- BANCO DE DADOS: SIMPOSIO
CREATE DATABASE Simposio;
GO

USE Simposio;
GO

-- Tabela Pessoa
CREATE TABLE Pessoa (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(255) NOT NULL,
    Instituicao VARCHAR(255)
);
GO

-- Tabela Simposio
CREATE TABLE Simposio (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(255) NOT NULL,
    Data DATE NOT NULL,
    Local VARCHAR(255) NOT NULL,
    Data_Inicio DATE NOT NULL,
    Data_Fim DATE NOT NULL
);
GO

-- Tabela Tema
CREATE TABLE Tema (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(255) NOT NULL
);
GO

-- Tabela Organizacao
CREATE TABLE Organizacao (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Id_Simposio INT NOT NULL,
    Id_Pessoa INT NOT NULL,
    FOREIGN KEY (Id_Simposio) REFERENCES Simposio(Id) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (Id_Pessoa) REFERENCES Pessoa(Id) ON DELETE CASCADE ON UPDATE CASCADE
);
GO

-- Tabela Comissao
CREATE TABLE Comissao (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Id_Tema INT NOT NULL UNIQUE,
    FOREIGN KEY (Id_Tema) REFERENCES Tema(Id) ON DELETE CASCADE ON UPDATE CASCADE
);
GO

-- Tabela Membro_Comissao
CREATE TABLE Membro_Comissao (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Id_Comissao INT NOT NULL,
    Id_Pessoa INT NOT NULL,
    FOREIGN KEY (Id_Comissao) REFERENCES Comissao(Id) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (Id_Pessoa) REFERENCES Pessoa(Id) ON DELETE CASCADE ON UPDATE CASCADE
);
GO

-- Tabela Artigo
CREATE TABLE Artigo (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Titulo VARCHAR(255) NOT NULL,
    Id_Tema INT NOT NULL,
    Data_Submissao DATE NOT NULL,
    FOREIGN KEY (Id_Tema) REFERENCES Tema(Id) ON DELETE CASCADE ON UPDATE CASCADE
);
GO

-- Tabela Autor
CREATE TABLE Autor (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Id_Pessoa INT NOT NULL,
    Id_Artigo INT NOT NULL,
    FOREIGN KEY (Id_Pessoa) REFERENCES Pessoa(Id) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (Id_Artigo) REFERENCES Artigo(Id) ON DELETE CASCADE ON UPDATE CASCADE
);
GO

-- Tabela Parecer
CREATE TABLE Parecer (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Id_Artigo INT NOT NULL,
    Id_Comissao INT NOT NULL,
    Status VARCHAR(50) NOT NULL, 
    Descricao VARCHAR(255) NOT NULL,
    Data_Emissao DATE NOT NULL,
    FOREIGN KEY (Id_Artigo) REFERENCES Artigo(Id) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (Id_Comissao) REFERENCES Comissao(Id) ON DELETE CASCADE ON UPDATE CASCADE
);
GO

-- Tabela Minicurso
CREATE TABLE Minicurso (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(255) NOT NULL,
    Id_Simposio INT NOT NULL,
    Id_Ministrante INT NOT NULL,
    Data DATE NOT NULL,
    Horario TIME NOT NULL,
    FOREIGN KEY (Id_Simposio) REFERENCES Simposio(Id) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (Id_Ministrante) REFERENCES Pessoa(Id) ON DELETE CASCADE ON UPDATE CASCADE
);
GO

-- Tabela Palestra
CREATE TABLE Palestra (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(255) NOT NULL,
    Id_Ministrante INT NOT NULL,
    Id_Simposio INT NOT NULL,
    Data DATE NOT NULL,
    Horario TIME NOT NULL,
    FOREIGN KEY (Id_Ministrante) REFERENCES Pessoa(Id) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (Id_Simposio) REFERENCES Simposio(Id) ON DELETE CASCADE ON UPDATE CASCADE
);
GO

-- Tabela Inscricao_Minicurso
CREATE TABLE Inscricao_Minicurso (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Id_Inscricao INT NOT NULL,
    Id_Minicurso INT NOT NULL,
    Data_Inscricao DATE NOT NULL,
    FOREIGN KEY (Id_Inscricao) REFERENCES Pessoa(Id) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (Id_Minicurso) REFERENCES Minicurso(Id) ON DELETE CASCADE ON UPDATE CASCADE
);
GO

-- Tabela Inscricao_Palestra
CREATE TABLE Inscricao_Palestra (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Id_Inscricao INT NOT NULL,
    Id_Palestra INT NOT NULL,
    Data_Inscricao DATE NOT NULL,
    FOREIGN KEY (Id_Inscricao) REFERENCES Pessoa(Id) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (Id_Palestra) REFERENCES Palestra(Id) ON DELETE CASCADE ON UPDATE CASCADE
);
GO

-- Trigger para impedir que o ministrante se inscreva na própria palestra
CREATE TRIGGER prevencao_ministrante_palestra
ON Inscricao_Palestra
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @Id_Inscricao INT, @Id_Palestra INT, @Id_Ministrante INT;

    SELECT @Id_Inscricao = Id_Inscricao, @Id_Palestra = Id_Palestra FROM inserted;

    SELECT @Id_Ministrante = Id_Ministrante FROM Palestra WHERE Id = @Id_Palestra;

    IF @Id_Inscricao = @Id_Ministrante
    BEGIN
        RAISERROR('O ministrante não pode se inscrever na própria palestra.', 16, 1);
        ROLLBACK TRANSACTION;
    END
    ELSE
    BEGIN
        INSERT INTO Inscricao_Palestra (Id_Inscricao, Id_Palestra, Data_Inscricao)
        SELECT Id_Inscricao, Id_Palestra, Data_Inscricao FROM inserted;
    END
END;
GO

-- Trigger para impedir que o ministrante se inscreva no próprio minicurso
CREATE TRIGGER prevencao_ministrante_minicurso
ON Inscricao_Minicurso
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @Id_Inscricao INT, @Id_Minicurso INT, @Id_Ministrante INT;

    SELECT @Id_Inscricao = Id_Inscricao, @Id_Minicurso = Id_Minicurso FROM inserted;

    SELECT @Id_Ministrante = Id_Ministrante FROM Minicurso WHERE Id = @Id_Minicurso;

    IF @Id_Inscricao = @Id_Ministrante
    BEGIN
        RAISERROR('O ministrante não pode se inscrever no próprio minicurso.', 16, 1);
        ROLLBACK TRANSACTION;
    END
    ELSE
    BEGIN
        INSERT INTO Inscricao_Minicurso (Id_Inscricao, Id_Minicurso, Data_Inscricao)
        SELECT Id_Inscricao, Id_Minicurso, Data_Inscricao FROM inserted;
    END
END;
GO

-- Trigger para validar a comissão do parecer
CREATE TRIGGER antes_parecer
ON Parecer
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @Id_Artigo INT, @Id_Comissao INT, @Id_Tema_Artigo INT;

    SELECT @Id_Artigo = Id_Artigo, @Id_Comissao = Id_Comissao FROM inserted;

    SELECT @Id_Tema_Artigo = Id_Tema FROM Artigo WHERE Id = @Id_Artigo;

    IF NOT EXISTS (
        SELECT 1 FROM Comissao WHERE Id = @Id_Comissao AND Id_Tema = @Id_Tema_Artigo
    )
    BEGIN
        RAISERROR('A comissão do parecer não corresponde ao tema do artigo.', 16, 1);
        ROLLBACK TRANSACTION;
    END
    ELSE
    BEGIN
        INSERT INTO Parecer (Id_Artigo, Id_Comissao, Status, Descricao, Data_Emissao)
        SELECT Id_Artigo, Id_Comissao, Status, Descricao, Data_Emissao FROM inserted;
    END
END;
GO
USE Simposio;
GO




INSERT INTO Simposio (Nome, Data, Local, Data_Inicio, Data_Fim) 
VALUES 
    ('Encontro de Inovação 2025', '2025-06-01', 'Instituto de Tecnologia', '2025-06-01', '2025-06-03');
GO

INSERT INTO Pessoa (Nome, Instituicao) 
VALUES 
    ('Lucas Almeida', 'Instituto de Tecnologia'),
    ('Fernanda Costa', 'Universidade Nacional'),
    ('Ricardo Lima', 'Instituto de Pesquisa'),
    ('Juliana Santos', 'Universidade do Centro'),
    ('Thiago Martins', 'Faculdade de Engenharia'),
    ('Camila Ferreira', 'Instituto de Tecnologia'),
    ('André Oliveira', 'Universidade Nacional'),
    ('Patrícia Rocha', 'Universidade do Norte'),
    ('Felipe Mendes', 'Instituto de Pesquisa'),
    ('Bianca Silva', 'Universidade do Centro');
GO

INSERT INTO Organizacao (Id_Simposio, Id_Pessoa) 
VALUES 
    (1, 1), (1, 2), (1, 3), (1, 4), (1, 5),
    (1, 6), (1, 7), (1, 8), (1, 9), (1, 10);
GO

INSERT INTO Tema (Nome) 
VALUES 
    ('Sistemas de Informação'),
    ('Comunicações'),
    ('Aprendizado de Máquina'),
    ('Proteção de Dados'),
    ('Desenvolvimento Ágil'),
    ('Infraestrutura em Nuvem'),
    ('Simulação Virtual'),
    ('Análise de Dados'),
    ('Conectividade Inteligente'),
    ('Arquitetura de Software');
GO

INSERT INTO Comissao (Id_Tema) 
VALUES 
    (1), (2), (3), (4), (5),
    (6), (7), (8), (9), (10);
GO

INSERT INTO Membro_Comissao (Id_Comissao, Id_Pessoa) 
VALUES 
    (1, 1), (1, 7), (2, 2), (3, 7), (4, 1),
    (5, 2), (6, 7), (7, 1), (8, 2), (9, 7);
GO

INSERT INTO Artigo (Titulo, Id_Tema, Data_Submissao) 
VALUES 
    ('Fundamentos de NoSQL', 1, '2025-04-01'),
    ('Segurança em Redes Avançadas', 2, '2025-04-02'),
    ('Inovações em IA', 3, '2025-04-03'),
    ('Criptografia Atual', 4, '2025-04-04'),
    ('Práticas Ágeis', 5, '2025-04-05'),
    ('Arquitetura de Nuvem', 6, '2025-04-06'),
    ('Experiências em Realidade Aumentada', 7, '2025-04-07'),
    ('Exploração de Big Data', 8, '2025-04-08'),
    ('IoT em Ambientes Urbanos', 9, '2025-04-09'),
    ('Modelos de Design de Software', 10, '2025-04-10');
GO

INSERT INTO Autor (Id_Pessoa, Id_Artigo) 
VALUES 
    (3, 1), (6, 1), (3, 2), (9, 3), (6, 4),
    (3, 5), (9, 6), (6, 7), (3, 8), (9, 9);
GO

INSERT INTO Minicurso (Nome, Id_Simposio, Id_Ministrante, Data, Horario) 
VALUES 
    ('Workshop de SQL', 1, 2, '2025-06-01', '09:00:00'),
    ('Workshop de Redes', 1, 5, '2025-06-01', '14:00:00'),
    ('Workshop de IA', 1, 2, '2025-06-02', '09:00:00'),
    ('Workshop de Segurança', 1, 5, '2025-06-02', '14:00:00'),
    ('Workshop de Nuvem', 1, 2, '2025-06-03', '09:00:00'),
    ('Workshop de Realidade Aumentada', 1, 5, '2025-06-03', '14:00:00'),
    ('Workshop de Big Data', 1, 2, '2025-06-01', '16:00:00'),
    ('Workshop de IoT', 1, 5, '2025-06-02', '16:00:00'),
    ('Workshop de Software', 1, 2, '2025-06-03', '16:00:00'),
    ('Workshop de Design', 1, 5, '2025-06-01', '11:00:00');
GO

INSERT INTO Palestra (Nome, Id_Ministrante, Id_Simposio, Data, Horario) 
VALUES 
    ('Palestra sobre IA Avançada', 2, 1, '2025-06-01', '10:00:00'),
    ('Palestra sobre Redes Futuras', 5, 1, '2025-06-01', '15:00:00'),
    ('Palestra sobre Nuvem e suas Aplicações', 2, 1, '2025-06-02', '10:00:00'),
    ('Palestra sobre Segurança Digital', 5, 1, '2025-06-02', '15:00:00'),
    ('Palestra sobre Big Data e Análise', 2, 1, '2025-06-03', '10:00:00'),
    ('Palestra sobre IoT e Cidades Inteligentes', 5, 1, '2025-06-03', '15:00:00'),
    ('Palestra sobre Desenvolvimento de Software', 2, 1, '2025-06-01', '17:00:00'),
    ('Palestra sobre Design de Software', 5, 1, '2025-06-02', '17:00:00'),
    ('Palestra sobre Realidade Aumentada e Virtual', 2, 1, '2025-06-03', '17:00:00'),
    ('Palestra sobre Sistemas de Informação', 5, 1, '2025-06-01', '12:00:00');
GO
