import { Show } from 'src/app/shows/shared/show.model';
import { TourWithEstimatedProfits } from './tour-with-estimated-profits.model';

export class TourWithEstimatedProfitsAndShows extends TourWithEstimatedProfits{
    shows: Show[];
  data: any;
}