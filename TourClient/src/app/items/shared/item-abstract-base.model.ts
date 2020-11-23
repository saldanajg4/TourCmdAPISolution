import { Ingredient } from 'src/app/ingredient/ingredient.model';

export class ItemAbstractBase {
    ItemName: string;
    Ingredients: Ingredient[];
    Description: string;
    Price: number;
}
