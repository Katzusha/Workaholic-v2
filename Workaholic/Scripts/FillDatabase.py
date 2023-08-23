from random import randrange
import datetime

numberofinserts = int(input("Number of inserts >>> "))
username = str(input("Username >>> "))
CreatedDate = datetime.datetime.today() + datetime.timedelta(days=numberofinserts * (-1))
for x in range(0, numberofinserts + 1):
    if (CreatedDate.strftime("%A") != "Sunday" and CreatedDate.strftime("%A") != "Saturday"):
        print(f"INSERT INTO Stamps (Id, Start, End, StampType, WorkStampId, Username, CreatedDate) VALUES ({x + 1}, '{randrange(6, 9)}:00:00', '{randrange(14, 18)}:00:00', 1, null, '{username}', '{CreatedDate}');")
    CreatedDate = CreatedDate + datetime.timedelta(days=1)

CreatedDate = datetime.datetime.today() + datetime.timedelta(days=numberofinserts * (-1))
for x in range(0, numberofinserts + 1):
    if (CreatedDate.strftime("%A") != "Sunday" and CreatedDate.strftime("%A") != "Saturday"):
        print(f"INSERT INTO Stamps (Start, End, StampType, WorkStampId, Username, CreatedDate) VALUES ('9:{randrange(0, 15)}:00', '9:{randrange(30, 59)}:00', 2, {x + 1}, '{username}', '{CreatedDate}');")
    CreatedDate = CreatedDate + datetime.timedelta(days=1)