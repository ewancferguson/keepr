export class Account {
  /**
   * @param {{ id: string; email: string; name: string; picture: string; coverImg: string }} data
   */
  constructor(data) {
    this.id = data.id;
    this.email = data.email;
    this.name = data.name;
    this.picture = data.picture;
    this.coverImg = data.coverImg;
    // TODO add additional properties if needed
  }
}
