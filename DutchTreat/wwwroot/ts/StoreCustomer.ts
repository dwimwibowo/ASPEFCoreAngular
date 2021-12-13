//export

class StoreCustomer {
    public visits: number = 0;
    private ourName: string;

    constructor(private firstName: string, private lastName: string) {

    }

    public showName() {
        alert(this.firstName + " " + this.lastName);
    }

    set name(val) {
        this.ourName = val;
    }

    get name() {
        return this.ourName;
    }
}