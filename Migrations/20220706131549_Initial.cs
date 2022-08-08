using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace thegioididong.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id_Category = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_Category = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id_Category);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    Id_manufacturer = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.Id_manufacturer);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id_Order = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_Cus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id_Order);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id_pro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pro_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Des = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_manufacturer = table.Column<int>(type: "int", nullable: false),
                    Id_Category = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Ram = table.Column<int>(type: "int", nullable: false),
                    Rom = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id_pro);
                    table.ForeignKey(
                        name: "FK_Product_Category_Id_Category",
                        column: x => x.Id_Category,
                        principalTable: "Category",
                        principalColumn: "Id_Category",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Manufacturers_Id_manufacturer",
                        column: x => x.Id_manufacturer,
                        principalTable: "Manufacturers",
                        principalColumn: "Id_manufacturer",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id_Com = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_pro = table.Column<int>(type: "int", nullable: false),
                    Name_cus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id_Com);
                    table.ForeignKey(
                        name: "FK_Comments_Product_Id_pro",
                        column: x => x.Id_pro,
                        principalTable: "Product",
                        principalColumn: "Id_pro",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id_image = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    link_image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_pro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id_image);
                    table.ForeignKey(
                        name: "FK_Images_Product_Id_pro",
                        column: x => x.Id_pro,
                        principalTable: "Product",
                        principalColumn: "Id_pro",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order_Detail",
                columns: table => new
                {
                    Id_D_Order = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Order = table.Column<int>(type: "int", nullable: false),
                    Id_pro = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_Detail", x => x.Id_D_Order);
                    table.ForeignKey(
                        name: "FK_Order_Detail_Order_Id_Order",
                        column: x => x.Id_Order,
                        principalTable: "Order",
                        principalColumn: "Id_Order",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_Detail_Product_Id_pro",
                        column: x => x.Id_pro,
                        principalTable: "Product",
                        principalColumn: "Id_pro",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Id_pro",
                table: "Comments",
                column: "Id_pro");

            migrationBuilder.CreateIndex(
                name: "IX_Images_Id_pro",
                table: "Images",
                column: "Id_pro");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Detail_Id_Order",
                table: "Order_Detail",
                column: "Id_Order");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Detail_Id_pro",
                table: "Order_Detail",
                column: "Id_pro");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Id_Category",
                table: "Product",
                column: "Id_Category");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Id_manufacturer",
                table: "Product",
                column: "Id_manufacturer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Order_Detail");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Manufacturers");
        }
    }
}
