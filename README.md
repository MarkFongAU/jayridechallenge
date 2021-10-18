To run

1. Unzip the folder
2. cd into `CodingChallenge` 
3. Run 
```
dotnet build
dotnet run
```
4. In `apiKey.txt`, add the `http://api.ipstack.com/`'s access key

5. Access the api at http://localhost:5000

e.g.
```
http://localhost:5000/candidate

http://localhost:5000/location?ip=123.243.155.45

http://localhost:5000/listings?passengerNumber=4
```