--- Product
INSERT [dbo].[Product] ([Id], [Name], [Description], [Weight], [Height], [Width], [Length]) 
VALUES (N'541f2bc6-850a-4f5e-abaa-315ec24c3c15', N'Product1', N'Product Description 1', 100.2, 54.4, 45, 67.3),
(N'cf0f951f-c650-42cc-a735-5a90f349f218', N'Product2', N'Product Description 2', 91.5, 44.5, 70.8, 58.1),
(N'1ad2e869-5bfa-402a-ac26-672c68a89d57', N'Product3', N'Product Description 3', 69.3, 84.1, 39.9, 66.4)

--- Order
INSERT [dbo].[Order] ([Id], [Status], [CreatedDate], [UpdatedDate], [ProductId]) 
VALUES (N'f41b5bc6-650a-4fke-a6aa-315eg24c3c15', 0, CAST(N'2022-03-21' AS DateTime), CAST(N'2022-04-21' AS DateTime), N'541f2bc6-850a-4f5e-abaa-315ec24c3c15'),
(N'cf0fvv1f-c650-42cc-a735-5a90f349f218', 1, CAST(N'2022-03-24' AS DateTime), CAST(N'2022-04-30' AS DateTime), N'541f2bc6-850a-4f5e-abaa-315ec24c3c15'),
(N'763cf8b8-4fdf-4fad-8d1d-a7cgg9b71a34', 1, CAST(N'2022-04-11' AS DateTime), CAST(N'2022-05-16' AS DateTime), N'cf0f951f-c650-42cc-a735-5a90f349f218'),
(N'd5631f81-1ff8-4fcc-a1d8-801142f5809c', 2, CAST(N'2022-04-09' AS DateTime), CAST(N'2022-04-20' AS DateTime), N'cf0f951f-c650-42cc-a735-5a90f349f218')