export class UrlRecord {
    constructor(id: string, originalUrl: string, shortUrl: string, createdDate: string, userName: string, userRole: string)
    {
        this.id = id;
        this.originalUrl = originalUrl;
        this.shortUrl = shortUrl;
        this.createdDate = createdDate;
        this.userName = userName;
        this.userRole = userRole;
    }

    id!: string;
    originalUrl!: string;
    shortUrl!: string;
    createdDate!: string;
    userName!: string;
    userRole!: string;
}