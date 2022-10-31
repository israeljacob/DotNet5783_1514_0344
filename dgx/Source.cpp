/*
name:
course : computer science.
exercise 9

The plan is built to keep details of bus lines that include:
line number, number of stops of the line, estimated travel time of the line
The user can choose a number from zero to seven
that defines what they want to do - each number expresses different instruction:
0-exit
1-Adds a line to the system and sorts the system again in ascending order.
It then prints all the lines with their details again
2- Deletes a line from the system, sorts again and prints
3-Prints everything in the system
4-Searches for a certain line
5-Calculate the average of total travel time
6-Calculates the average stations of all lines
7-Prints the number of line that has the shortest travel time
*/
#include <iostream>
using namespace std;

enum choices {
	EXIT, ADD, DELETE, PRINT, SEARCH, AVG_TRAVEL, AVG_STOPS,
	SHORTEST_TRAVEL
};
/* sorts the line in ascending  order*/
void inserSort(int** buses, int numbuses)
{
	for (int i = 1; i < numbuses; i++)
	{
		for (int j = 0; j < numbuses - i; j++)
		{
			if (*(*(buses + j)) > *(*(buses + j + 1)))
			{
				int tempLine = *(*(buses + j));
				*(*(buses + j)) = *(*(buses + j + 1));
				*(*(buses + j + 1)) = tempLine;
				int tempStopts = *(*(buses + j) + 1);
				*(*(buses + j) + 1) = *(*(buses + j + 1) + 1);
				*(*(buses + j + 1) + 1) = tempStopts;
				int tempDuration = *(*(buses + j) + 2);
				*(*(buses + j) + 2) = *(*(buses + j + 1) + 2);
				*(*(buses + j + 1) + 2) = tempDuration;
			}
		}
	}
}
// Searches if there is such a line number:  
// If yes, returns the number. If not, returns -1.
int search(int** buses, int numBuses, int line)
{
	int result = -1;
	int low = 0;
	int high = numBuses - 1; // N - # of elements
	while (low <= high)
	{
		int mid = (low + high) / 2;
		if (line == *(*(buses + mid)))
		{
			result = mid;
			break;
		}
		else if (line > *(*(buses + mid)))
		{
			low = mid + 1;
		}
		else { high = mid - 1; }
	}
	return result;
}
// Adds lines to the list. For each one, the progrem gets:
// The kine number, the stops number and estimated travel time
void addLine(int** buses, int& numBuses, int line, int stops,
	int durationOfRide)
{

	if (numBuses < 50 && search(buses, numBuses, line) == -1)
	{
		*(*(buses + numBuses)) = line;
		*(*(buses + numBuses) + 1) = stops;
		*(*(buses + numBuses) + 2) = durationOfRide;
		numBuses++;
		inserSort(buses, numBuses);
	}
}
// Removes from the list one line eccording to the users chois.
void deleteLine(int** buses, int& numBuses, int line)
{
	int check = search(buses, numBuses, line);
	if (check != -1)
	{
		for (int i = check; i < numBuses; i++)
		{
			*(*(buses + i)) = *(*(buses + i + 1));
			*(*(buses + i) + 1) = *(*(buses + i + 1) + 1);
			*(*(buses + i) + 2) = *(*(buses + i + 1) + 2);
		}
		numBuses--;
	}
}
// Calculates the average of the durations of all the lines.
float averageTravel(int** buses, int numBuses)
{
	int sum = 0;
	float average = 0;
	for (int i = 0; i < numBuses; i++)
	{
		sum += *(*(buses + i) + 2);
	}
	if (numBuses != 0)
		average = float(sum / numBuses);
	return average;
}
// Calculates the average of the stops of all the lines.
float averageStops(int** buses, int numBuses)
{
	int sum = 0;
	float average = 0;
	for (int i = 0; i < numBuses; i++)
	{
		sum += *(*(buses + i) + 1);
	}
	if (numBuses != 0)
		average = float(sum / numBuses);
	return average;
}
// prints the line number that has the shortest duration.
int shortest(int** buses, int numBuses)
{
	int shortestTravel = *(*(buses));
	if (numBuses == 0)
		shortestTravel = -1;
	for (int i = 1; i < numBuses - 1; i++)
	{
		if (*(*(buses + i)) + 2 < *(*(buses + i + 1) + 2))
			shortestTravel = *(*(buses + i));
	}
	return shortestTravel;
}


// prints out the buses that are saved in the database
void print(int** buses, int numBuses) {

	for (int i = 0; i < numBuses; i++) { // for each bus that is present in the database
		for (int j = 0; j < 3; j++) { // prints the 3 pieces of data of the bus
			cout << *(*(buses + i) + j) << " ";
		}
		cout << endl;
	}
}


int  main() {
	int** buses = new int* [50];
	for (int i = 0; i < 50; i++)
		buses[i] = new int[3];
	int numBuses = 0;
	int line;
	int stops;
	int durationOfRide;
	int choice;
	do {
		cout << "enter 0-7" << endl;
		cin >> choice;
		switch (choice) {
		case ADD: // add a line to the array of buses
			cout << "enter the line to add" << endl;
			cin >> line;
			while (line <= 0)
			{
				cout << "ERROR" << endl;
				cin >> line;
			}
			cout << "enter the number of stops" << endl;
			cin >> stops;
			while (stops <= 0)
			{
				cout << "ERROR" << endl;
				cin >> stops;
			}
			cout << "enter the duration of the ride" << endl;
			cin >> durationOfRide;
			while (durationOfRide <= 0)
			{
				cout << "ERROR" << endl;
				cin >> durationOfRide;
			}

			addLine(buses, numBuses, line, stops, durationOfRide);
			print(buses, numBuses);
			break;

		case DELETE:	// delete a line from the array of buses	
			cout << "enter the line to delete" << endl;
			cin >> line;
			deleteLine(buses, numBuses, line);
			print(buses, numBuses);
			break;
		case PRINT: // print all lines
			print(buses, numBuses);
			break;


		case SEARCH: // search for a particular line
			cout << "enter the line to search for" << endl;
			cin >> line;
			cout << search(buses, numBuses, line) << endl;
			break;
		case AVG_TRAVEL: // calculate average travel time of all buses
			cout << averageTravel(buses, numBuses) << endl;
			break;
		case AVG_STOPS:  // calcuate average stops of all buses
			cout << averageStops(buses, numBuses) << endl;
			break;
		case SHORTEST_TRAVEL:// calculate the bus with the shortest travel time
			cout << shortest(buses, numBuses) << endl;
			break;
		case EXIT: break;   // exit the program

		default:  cout << "ERROR" << endl;
		}// switch
	} while (choice != 0);
	return 0;
}
