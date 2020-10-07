import { Item } from 'src/app/items/shared/item.model';
import { Order } from './order.model';

export class OrderWithItems extends Order{
    Items: Item[];
}
