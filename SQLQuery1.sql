IF EXISTS (SELECT *FROM Calendar WHERE titel ='hallo', terminid ='2')
	UPDATE Calendar SET titel ='neu'
ELSE
	INSERT INTO Calendar (titel) VALUES ('hallo');