INSERT INTO [User] (Email, Username, Password) VALUES
('john@gmail.com', 'John', 'j'),
('bob@gmail.com', 'bob', 'b')

INSERT INTO CardSet (Name, UserOwnerId) VALUES
('U.S Presidents', 1),
('World History', 1),
('Biology', 2),
('Chemistry', 2)

INSERT INTO Cards (FrontCard, BackCard, CardSetId) VALUES
('Who was the 1st president of the U.S?', 'George Washington', 1),
('What president said: "Speak softly and carry a big stick"?', 'Theodore Roosevelt', 1),
('Who was behind the scandal known as "Watergate"?', 'Richard Nixon', 1),

('What countries worked with Germany in WW2?', 'Russia, Italy', 2),
('(True/False) One U.S state once belonged to Mexico', 'False', 2),

('Chemical known as "bases" within human genes are represented by which 4 letters?', 'A, C, T, G', 3),
('What does "DNA" stand for?', 'Deoxyribonucleic acid', 3),

('What elements does H20 contain?', 'Hydrogen and Oxygen', 4),
('What does each of the NFPA color codes represent?', 'Health (blue), Flamability (red), Specific hazard (white), Instability (yellow)', 4)
