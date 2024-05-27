import Error from "./error"

export default class Result<T = null> {
    public isSuccess: boolean

    constructor(
        public errors: Error[],
        public data: T = <any>null
    ) {
        this.isSuccess = errors.length <= 0
    }

    public static success<T>(value: T): Result<T> {
        return new Result<T>([], value)
    }

    public static fail(error: Error): Result {
        return new Result([error])
    }
}