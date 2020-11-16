export interface Statistics {
  appliedActivities: number;
  clientsVisited: number;
  clientsRegistered: number;
  profits: number;
}

export interface DayStatistics extends Statistics {
  id: number;
  date: Date;
}

export interface MontStatistics extends Statistics {
  id: number;
  date: Date;

  dayStatistics?: DayStatistics[];
}

export interface YearStatistics extends Statistics {
  id: number;
  year: number;

  montStatistics?: MontStatistics[];
}
