CREATE TABLE [Roles]
(
	[id] INT PRIMARY KEY IDENTITY
	, [name] NVARCHAR(25) NOT NULL
);
CREATE TABLE [Users]
(
	[id] INT PRIMARY KEY IDENTITY
	, [login] NVARCHAR(25) NOT NULL
	, [password] NVARCHAR(225) NOT NULL
	, [role_id] INT REFERENCES [Roles]([id])
);
CREATE TABLE [Categories]
(
	[id] INT PRIMARY KEY IDENTITY
	, [name] NVARCHAR(25) NOT NULL
);
CREATE TABLE [Articles]
(
	[id] INT PRIMARY KEY IDENTITY
	, [title] NVARCHAR(25) NOT NULL
	, [description] NVARCHAR(25) NOT NULL
	, [image_url] NVARCHAR(225)
	, [image_base64] VARCHAR(MAX)
	, [category_id] INT REFERENCES [Categories]([id])
	, [price] DECIMAL(10,2) NOT NULL
	, [quantity] INT NOT NULL
);
CREATE TABLE [Colors]
(
	[id] INT PRIMARY KEY IDENTITY
	, [name] NVARCHAR(25) NOT NULL
);
CREATE TABLE [Sizes]
(
	[id] INT PRIMARY KEY IDENTITY
	, [name] NVARCHAR(25) NOT NULL
);
CREATE TABLE [Brand]
(
	[id] INT PRIMARY KEY IDENTITY
	, [name] NVARCHAR(25) NOT NULL
);
CREATE TABLE [Article_details]
(
	[id] INT PRIMARY KEY IDENTITY
	, [article_id] INT REFERENCES [Articles]([id])
	, [color_id] INT REFERENCES [Colors]([id])
	, [size_id] INT REFERENCES [Sizes]([id])
	, [brand_id] INT REFERENCES [Brand]([id])
);