import { ShowForCreation } from 'src/app/shows/show-for-creation.model';
import { TourForCreation } from './tour-for-creation.model';

export class TourWithShowsForCreation extends TourForCreation{
    shows: ShowForCreation[];
}
