import { Company } from './company';

export class Experience{
    companyId:         number;
    startDate:         Date;
    endDate:           null;
    isCurrentPosition: boolean;
    company:           Company;
}