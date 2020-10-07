import { ShowForCreation } from 'src/app/shows/show-for-creation.model';
import { TourWithManagerForCreation } from './tour-with-manager-for-creation.model';

export class TourWithManagerAndShowsForCreation extends TourWithManagerForCreation {
    shows: ShowForCreation[];
}
